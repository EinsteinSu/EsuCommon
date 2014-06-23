using System;
using System.IO;
using Caliburn.Micro;

namespace UpgradeToolKit
{
  public class CopyViewModel : PropertyChangedBase
  {
    private string message;
    private Progress progress;

    public string Message
    {
      get { return message; }
      set
      {
        if (value == message) return;
        message = value;
        NotifyOfPropertyChange(() => Message);
      }
    }

    public Progress Progress
    {
      get { return progress; }
      set
      {
        if (Equals(value, progress)) return;
        progress = value;
        NotifyOfPropertyChange(() => Progress);
      }
    }

    public void StartCopy()
    {
      Message = "正在开始复制 ...";
      string tempPath = string.Format("{0}\\UpdateTemp", Environment.CurrentDirectory);
      if (Directory.Exists(tempPath))
      {
        EsuFileInfoCollection collection = tempPath.GetFilesFromDirectory(true);
        Progress = new Progress(collection.Count);

        #region copy files

        foreach (EsuFileInfo esuFileInfo in collection.GetDirectoryList())
        {
          string directory = string.Format("{0}\\{1}", Environment.CurrentDirectory, esuFileInfo.RelativeFileName);
          if (!Directory.Exists(directory))
          {
            Message = string.Format("正在创建文件夹{0} ...", directory);
            Directory.CreateDirectory(directory);
          }
          Progress.StepAdd();
        }
        foreach (EsuFileInfo esuFileInfo in collection.GetFileList())
        {
          Message = string.Format("正在写入文件{0} ...", esuFileInfo.RelativeFileName);
          string fileName = string.Format("{0}\\{1}", Environment.CurrentDirectory, esuFileInfo.RelativeFileName);
          File.Copy(esuFileInfo.FileName, fileName, true);
          Progress.StepAdd();
        }

        #endregion

        #region delete files

        Message = "正在清理文件 ...";
        Progress = new Progress(collection.Count);
        foreach (EsuFileInfo esuFileInfo in collection.GetFileList())
        {
          if (File.Exists(esuFileInfo.FileName))
            File.Delete(esuFileInfo.FileName);
          Progress.StepAdd();
        }
        foreach (EsuFileInfo esuFileInfo in collection.GetDirectoryList())
        {
          if (Directory.Exists(esuFileInfo.FileName))
            Directory.Delete(esuFileInfo.FileName);
          Progress.StepAdd();
        }

        #endregion
      }
    }
  }

  public class Progress : PropertyChangedBase
  {
    private readonly int maxStep;
    private int step;

    public Progress(int maxStep)
    {
      this.maxStep = maxStep;
      step = 0;
    }

    public int MaxStep
    {
      get { return maxStep; }
    }

    public int Step
    {
      get { return step; }
      set
      {
        if (value == step) return;
        step = value;
        NotifyOfPropertyChange(() => Step);
      }
    }

    public void StepAdd()
    {
      if (Step < MaxStep)
        Step++;
    }
  }
}