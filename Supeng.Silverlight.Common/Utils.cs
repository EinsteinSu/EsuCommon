using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Supeng.Silverlight.Common
{
  public static class Utils
  {
    public static bool DeleteMessage(string deleteEntity)
    {
      return MessageBox.Show(string.Format("是否删除该[{0}]?", deleteEntity), "删除", MessageBoxButton.OKCancel) ==
             MessageBoxResult.OK;
    }

    public static FrameworkElement GetFrameworkElement(this Dictionary<string, Type> dictionary, string key)
    {
      if (dictionary.ContainsKey(key))
      {
        var element = Activator.CreateInstance(dictionary[key]) as FrameworkElement;
        if (element != null)
          return element;
      }
      return new TextBox();
    }
  }
}