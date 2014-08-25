using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Supeng.Mail.Tests
{
  class Program
  {
    static void Main(string[] args)
    {
      MailAddress from = new MailAddress("ynhengrui@163.com", "云南恒锐消防咨询"); //邮件的发件人
      MailMessage mail = new MailMessage();
      mail.From = from;
      mail.Subject = "Subject";
      mail.To.Add(new MailAddress("76507593@qq.com"));
      //mail.CC.Add("");
      mail.Body = "<h1> this is a title</h1><p> this p1 </p>";
      mail.BodyEncoding = Encoding.UTF8;
      mail.IsBodyHtml = true;
      mail.Priority = MailPriority.Normal;
      #region attachment
      //string fileName = txtUpFile.PostedFile.FileName.Trim();
      //fileName = "D:/UpFile/" + fileName.Substring(fileName.LastIndexOf("/") + 1);
      //txtUpFile.PostedFile.SaveAs(fileName); // 将文件保存至服务器
      //mail.Attachments.Add(new Attachment(fileName));

      #endregion

      var client = new SmtpClient();
      client.Host = "smtp.163.com";
      //client.Port = 25;
      client.UseDefaultCredentials = false;
      client.Credentials = new System.Net.NetworkCredential("ynhengrui@163.com", "hengrui");
      client.DeliveryMethod = SmtpDeliveryMethod.Network;
      client.Send(mail);
    }
  }
}
