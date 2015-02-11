using System;
using System.IO;

namespace Supeng.Common.IOs
{
    public class EsuLogs
    {
        protected string FileName = string.Format("{0}{1}.log", DirectoryHelper.LogDirectory, DateTime.Now.ToString("yyyyMMdd"));
        public EsuLogs(string defaultName)
        {
            if (!string.IsNullOrEmpty(defaultName))
                FileName = string.Format("{0}{1}{2}.log", DirectoryHelper.LogDirectory, defaultName, DateTime.Now.ToString("yyyyMMdd"));
        }

        public void WriteLog(string log)
        {
            File.AppendAllText(FileName, log + Environment.NewLine);
        }

        public void WriteLogWithDatetime(string log)
        {
            WriteLog(string.Format("[{0}]:{1}", DateTime.Now, log));
        }
    }
}
