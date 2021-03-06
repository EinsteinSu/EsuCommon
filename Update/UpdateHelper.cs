﻿
using System;
using Supeng.Common.Interfaces;

namespace Update
{
  public static class UpdateHelper
  {
    /// <summary>
    /// start update
    /// </summary>
    /// <param name="directoryName">directory name of server</param>
    /// <param name="upgrade">how to update</param>
    /// <param name="success"></param>
    /// <param name="failed"></param>
    /// <param name="cancel"></param>
    public static void ShowUpdate(string directoryName, IUpgrade upgrade, string startApp = null, Action success = null, Action failed = null, Action cancel = null)
    {
      var view = new UpdateWindowView();
      var viewModel = new UpdateWindowViewModel(directoryName, upgrade, startApp, success, failed, cancel);
      view.DataContext = viewModel;
      view.Show();
    }
  }
}