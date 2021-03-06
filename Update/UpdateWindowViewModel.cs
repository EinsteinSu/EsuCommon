﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Interfaces;
using Supeng.Common.IOs;
using Supeng.Common.Threads;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Update
{
  public class UpdateWindowViewModel : EsuInfoBase
  {
    private readonly Action cancel;
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand closeCommand;
    private readonly string directoryName;
    private readonly Action failed;
    private readonly TaskScheduler scheduler;
    private readonly string startApp;
    private readonly Action success;
    private readonly DelegateCommand updateCommand;
    private readonly IUpgrade upgrade;
    private bool canUpdate = true;
    private UpdateFile currentUpdate;
    private string message;
    private EsuStepProgressViewModel progress;
    private CancellationTokenSource tokenSource;
    private UpdateFileCollection updateCollection;

    public UpdateWindowViewModel(string directoryName, IUpgrade upgrade, string startApp = "", Action success = null,
      Action failed = null, Action cancel = null
      )
    {
      this.directoryName = directoryName;
      this.upgrade = upgrade;
      this.startApp = startApp;
      this.success = success;
      this.failed = failed;
      this.cancel = cancel;
      UpdateCollection = new UpdateFileCollection(new List<EsuUpgradeInfo>());
      updateCommand = new DelegateCommand(Update, () => true);
      closeCommand = new DelegateCommand(Close, () => true);
      cancelCommand = new DelegateCommand(Cancel, () => true);
      scheduler = TaskScheduler.Current;
    }

    #region properties

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

    public UpdateFileCollection UpdateCollection
    {
      get { return updateCollection; }
      set
      {
        updateCollection = value;
        Progress = new EsuStepProgressViewModel(updateCollection.Count);
        NotifyOfPropertyChange(() => UpdateCollection);
        NotifyOfPropertyChange(() => Progress.MaxStep);
      }
    }

    public UpdateFile CurrentUpdate
    {
      get { return currentUpdate; }
      set
      {
        if (Equals(value, currentUpdate)) return;
        currentUpdate = value;
        NotifyOfPropertyChange(() => CurrentUpdate);
      }
    }

    public bool CanUpdate
    {
      get { return canUpdate; }
      set
      {
        if (value.Equals(canUpdate)) return;
        canUpdate = value;
        NotifyOfPropertyChange(() => CanUpdate);
      }
    }

    public EsuStepProgressViewModel Progress
    {
      get { return progress; }
      set
      {
        if (Equals(value, progress)) return;
        progress = value;
        NotifyOfPropertyChange(() => Progress);
      }
    }

    #endregion

    #region commands

    public DelegateCommand UpdateCommand
    {
      get { return updateCommand; }
    }

    public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    public DelegateCommand CloseCommand
    {
      get { return closeCommand; }
    }

    #endregion


    public async void Update()
    {
      if (upgrade == null)
        return;
      Message = "正在获取升级数据 ...";
      CanUpdate = false;
      tokenSource = new CancellationTokenSource();
      await ThreadHelper.StartTask(() =>
      {
        EsuUpgradeInfoCollection serviceList = upgrade.GetServiceFileCollection();
        string tempPath = string.Format("{0}\\UpdateTemp", Environment.CurrentDirectory);
        if (!Directory.Exists(tempPath))
          Directory.CreateDirectory(tempPath);
        UpdateCollection = new UpdateFileCollection(serviceList.GetDifferent(upgrade.GetLocalFileCollection()));
        if (UpdateCollection.Count == 0)
        {
          MessageBox.Show("未发现可更新的组件.");
          return;
        }
        Message = "获取数据完成，正在准备升级 ...";

        #region download files to local path

        foreach (UpdateFile updateFile in UpdateCollection.Where(w => w.FileInfo.Type == FileType.Directory))
        {
          Message = string.Format("正在创建文件夹{0} ...", updateFile.FileInfo.Name);
          CurrentUpdate = updateFile;
          Directory.CreateDirectory(string.Format(@"{0}\{1}", tempPath, updateFile.FileInfo.RelativeFileName));
          updateFile.Upgradable = true;
          Progress.StepAdd();
        }

        foreach (UpdateFile updateFile in UpdateCollection.Where(w => w.FileInfo.Type == FileType.File))
        {
          Message = string.Format("正在下载文件{0} ...", updateFile.FileInfo.Name);
          CurrentUpdate = updateFile;
          string source = string.Format(@"{0}\{1}", tempPath, updateFile.FileInfo.RelativeFileName);
          //string target = string.Format(@"{0}\{1}", Environment.CurrentDirectory, updateFile.FileInfo.RelativeFileName);
          upgrade.GetFileBytes(updateFile.FileInfo.RelativeFileName)
            .ByteToFile(source);
          updateFile.Upgradable = true;
          Thread.Sleep(100);
          Progress.StepAdd(10);
        }
      });

        #endregion

      Message = "组件下载完成，正在启动更新组件 ...";
      if (success != null)
        success();
      try
      {
        string copyApplicationFileName = string.Format("{0}\\UpdateToolkit.exe", Environment.CurrentDirectory);
        if (File.Exists(copyApplicationFileName))
          Process.Start(copyApplicationFileName, startApp);
        else
          MessageBox.Show("未发现更新组件，请联系系统管理员。");
        Environment.Exit(0);
      }
      catch
      {
        MessageBox.Show("复制组件发生错误，请联系系统管理员。");
      }
    }

    private void Cancel()
    {
      //tokenSource.Cancel();
    }

    private void Close()
    {
      Application.Current.Shutdown();
    }
  }
}