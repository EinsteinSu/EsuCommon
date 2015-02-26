using System.Collections.Generic;
using Supeng.Http.Common;

namespace Supeng.SmsMessage.Common
{
  public class MessageSender
  {
    private readonly string userName;
    private readonly string password;
    private const string Url = "http://www.xbsms.com/SmsPort/SmsSendPort.aspx?username={0}&userpwd={1}&mobilelist={2}&content={3}";
    private readonly Dictionary<int, string> messageResults;
    public MessageSender(string userName, string password)
    {
      this.userName = userName;
      this.password = password;
      messageResults = new Dictionary<int, string>
            {
                {1, "成功"},
                {2, "访问数据库写入数据错误"},
                {3, "登录账户错误，或者该用户不存在,或者被停用"},
                {4, "号码太长不能超过3000条一次提交"},
                {5, "余额不足"},
                {6, "参数不全"},
                {7, "帐户数据异常"},
                {8, "JSON格式数据不正确"},
                {9, "内容超过最大长度300字"},
            };
    }

    public string SendMessage(string mobiles, string content)
    {
      string url = string.Format(Url, userName, password, mobiles, content);
      using (var data = new EsuWebClient())
      {
        var result = data.GetString(url);
        var messageResult = int.Parse(result);
        return GetResult(messageResult);
      }
    }

    public string SendMessage(IEnumerable<string> mobileList, string content)
    {
      var mobiles = string.Join(",", mobileList);
      return SendMessage(mobiles, content);
    }

    protected string GetResult(int key)
    {
      if (messageResults.ContainsKey(key))
        return messageResults[key];
      return "Unknow exception";
    }
  }
}
