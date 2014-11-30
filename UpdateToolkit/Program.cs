using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateToolkit
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("正在展开升级文件...");
      string tempPath = string.Format("{0}\\UpdateTemp", Environment.CurrentDirectory);
      if (Directory.Exists(tempPath))
      {
        string appName = string.Empty;
        if (args != null && args.Any())
        {
          appName = args[0];
        }
        //Console.WriteLine("正在查找{0}的进程...", appName);
        if (!string.IsNullOrEmpty(appName))
        {
          foreach (var process in Process.GetProcessesByName(appName.Replace(".exe", "")))
          {
            Console.WriteLine("正在关闭{0}", process.ProcessName);
            process.Kill();
            process.Close();
          }
        }
        var collection = EsuUpgradeInfoHelper.GetUpgradeCollection(tempPath);
        int count = collection.Count;
        int current = 0;

        #region copy files

        foreach (var esuFileInfo in collection.GetDirectoryList())
        {
          string directory = string.Format("{0}\\{1}", Environment.CurrentDirectory, esuFileInfo.RelativeFileName);
          if (!Directory.Exists(directory))
          {
            Console.WriteLine("正在创建文件夹{0} ...", directory);
            Directory.CreateDirectory(directory);
          }
          current++;
          DisplayPercent("展开文件", count, current);
        }
        foreach (var esuFileInfo in collection.GetFileList())
        {
          Console.WriteLine("正在展开文件{0} ...", esuFileInfo.RelativeFileName);
          string fileName = string.Format("{0}\\{1}", Environment.CurrentDirectory, esuFileInfo.RelativeFileName);
          try
          {
            //关闭正在使用的dll
            using (Stream str = File.Create(fileName))
            {
            }
            File.Copy(esuFileInfo.FileName, fileName, true);
          }
          catch
          {
            Console.WriteLine("复制{0}失败，可能是由于系统进程未结束造成的，请重新升级或者联系系统管理员。", esuFileInfo.Name);
          }

          current++;
          DisplayPercent("展开文件", count, current);
        }

        #endregion

        #region delete files

        if (args != null && args.Count() > 1 && args[1].Equals("true", StringComparison.InvariantCultureIgnoreCase))
        {
          Console.WriteLine("正在清理文件 ...");
          current = 0;
          foreach (var esuFileInfo in collection.GetFileList())
          {
            if (File.Exists(esuFileInfo.FileName))
            {
              try
              {
                File.Delete(esuFileInfo.FileName);
              }
              catch
              {
                Console.WriteLine("文件{0}正在被占用;", esuFileInfo.FileName);
              }
            }
            current++;
            DisplayPercent("删除文件", count, current);
          }
          foreach (var esuFileInfo in collection.GetDirectoryList())
          {
            if (Directory.Exists(esuFileInfo.FileName))
            {
              try
              {
                Directory.Delete(esuFileInfo.FileName);
              }
              catch
              {

                Console.WriteLine("文件{0}正在被占用;", esuFileInfo.FileName);
              }
            }
            current++;
            DisplayPercent("删除文件", count, current);
          }
        }
        #endregion

        Console.WriteLine("升级完成！所有升级文件已展开。请按回车键打开程序...");
        Console.Read();
        if (args != null && args.Any())
        {
          if (!string.IsNullOrEmpty(args[0]))
          {
            Process.Start(string.Format("{0}\\{1}", Environment.CurrentDirectory, args[0]));
          }
        }
      }
    }

    static void DisplayPercent(string title, int count, int current)
    {
      Console.WriteLine("{0}:{1:F0}%", title, ((double)current / (double)count) * 100D);
    }
  }
}
