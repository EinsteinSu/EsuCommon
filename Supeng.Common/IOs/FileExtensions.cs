using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Supeng.Common.IOs
{
  public static class FileExtensions
  {
    public static T LoadDataFromXml<T>(this string fileName)
    {
      if (!File.Exists(fileName))
        return default(T);
      using (var reader = new StreamReader(fileName))
      {
        var xs = new XmlSerializer(typeof(T));
        return (T)xs.Deserialize(reader);
      }
    }

    public static T LoadDataFromXmlInDataPath<T>(this string fileName)
    {
      return string.Format("{0}\\{1}", DirectoryHelper.DataDirectory, fileName).LoadDataFromXml<T>();
    }

    public static T LoadDataFromText<T>(this string fileName)
    {
      if (!File.Exists(fileName))
        return default(T);
      return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
    }

    public static T LoadDataFromTextInDataPath<T>(this string fileName)
    {
      return string.Format("{0}\\{1}", DirectoryHelper.DataDirectory, fileName).LoadDataFromText<T>();
    }

    public static ImageSource GetImageSourceByName(this string fileName)
    {
      if (File.Exists(fileName))
        return new BitmapImage(new Uri(fileName, UriKind.Absolute));
      return null;
    }
  }
}
