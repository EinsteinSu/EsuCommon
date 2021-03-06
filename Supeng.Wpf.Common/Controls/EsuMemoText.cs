﻿using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;

namespace Supeng.Wpf.Common.Controls
{
  public class EsuMemoText : TextEdit
  {
    public EsuMemoText()
    {
      VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
      HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
      VerticalContentAlignment = VerticalAlignment.Top;
      HorizontalContentAlignment = HorizontalAlignment.Left;
      AcceptsReturn = true;
    }
  }
}