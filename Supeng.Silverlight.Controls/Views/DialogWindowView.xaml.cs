﻿using System.Windows;
using System.Windows.Controls;

namespace Supeng.Silverlight.Controls.Views
{
  public partial class DialogWindowView : ChildWindow
  {
    public DialogWindowView()
    {
      InitializeComponent();
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}