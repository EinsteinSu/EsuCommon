﻿using SqlScriptGenerator.ViewModels;

namespace SqlScriptGenerator
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      DataContext = new ScriptGeneratorViewModel();
    }
  }
}