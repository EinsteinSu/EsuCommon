using System;
using System.IO;
using System.IO.IsolatedStorage;
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
  }
}