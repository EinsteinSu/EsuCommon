using System.IO;
using System.Windows.Markup;
using System.Xml;
using Supeng.Common.IOs;

namespace Supeng.Wpf.Common.Templates
{
  public static class TeamplateExtensions
  {
    public static T GetDataTemplateFromFile<T>(this string templateName)
    {
      string fileName = string.Format("{0}{1}.xml", DirectoryHelper.TemplateDirectory, templateName);
      if (File.Exists(fileName))
      {
        string text = File.ReadAllText(fileName);
        var reader = new StringReader(text);
        XmlReader xmlReader = XmlReader.Create(reader);
        return (T) XamlReader.Load(xmlReader);
      }
      return default(T);
    }
  }
}