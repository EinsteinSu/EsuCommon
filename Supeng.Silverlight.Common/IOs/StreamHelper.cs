using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Supeng.Silverlight.Common.IOs
{
  public static class StreamHelper
  {
    public static void WriteText(string fileName, string text)
    {
      IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
      Stream stream = new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, isf);
      TextWriter writer = new StreamWriter(stream);
      writer.WriteLine(text);
      writer.Close();
      stream.Close();
    }

    public static string ReadText(string fileName)
    {
      string text;
      try
      {
        IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
        if (isf.FileExists(fileName))
        {
          Stream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, isf);
          TextReader reader = new StreamReader(stream);
          string sLine = reader.ReadLine();
          text = sLine;
          reader.Close();
          stream.Close();
        }
        else
        {
          return string.Empty;
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      return text;
    }

    public static T ReadFromText<T>(this string fileName)
    {
      string data = ReadText(fileName);
      var result = JsonConvert.DeserializeObject<T>(data);
      return result;
    }

    public static T ReadJosonFromLocalSource<T>(this Uri sourceUri)
    {
      StreamResourceInfo info = Application.GetResourceStream(sourceUri);
      if (info != null)
      {
        using (var reader = new StreamReader(info.Stream))
        {
          string data = reader.ReadToEnd();
          return JsonConvert.DeserializeObject<T>(data);
        }
      }
      return default(T);
    }

    public static T ReadXmlFromLocalSource<T>(this Uri sourceUri)
    {
      StreamResourceInfo info = Application.GetResourceStream(sourceUri);
      if (info != null)
      {
        using (var reader = new StreamReader(info.Stream))
        {
          var xs = new XmlSerializer(typeof (T));
          return (T) xs.Deserialize(reader);
        }
      }
      return default(T);
    }
  }
}