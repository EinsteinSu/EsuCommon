using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Supeng.Email
{
  public class EmailSender
  {
    private readonly string userName;
    private readonly string password;
    private readonly string host;
    private readonly MailAddress fromAddress;
    public EmailSender(string emailAddress, string displayName, string userName, string password, string host)
    {
      this.userName = userName;
      this.password = password;
      this.host = host;
      fromAddress = new MailAddress(emailAddress, displayName);
    }

    public bool SendEmail(string subject, IList<string> userList, IList<string> ccList, string body, MailPriority priority = MailPriority.Normal)
    {
      var mail = new MailMessage
      {
        From = fromAddress,
        Subject = subject,
        Body = body,
        BodyEncoding = Encoding.UTF8,
        IsBodyHtml = true,
        Priority = priority
      };

      foreach (var user in userList)
        mail.To.Add(user);
      foreach (var cc in ccList)
        mail.CC.Add(cc);

      var client = new SmtpClient
      {
        Host = host,
        UseDefaultCredentials = false,
        Credentials = new System.Net.NetworkCredential(userName, password),
        DeliveryMethod = SmtpDeliveryMethod.Network
      };
      try
      {
        client.Send(mail);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
