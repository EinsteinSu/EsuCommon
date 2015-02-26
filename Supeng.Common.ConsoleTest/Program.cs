using System;
using System.Globalization;
using Microsoft.Win32;
using Supeng.Common.Threads;

namespace Supeng.Common.ConsoleTest
{
  class Program
  {
    static void Main(string[] args)
    {
      //Registry.LocalMachine.SetValue(@"EsuTest\Config", "Test");
      //var value = Registry.LocalMachine.GetValue(@"EsuTest\Config");
      //Console.WriteLine(value);
      var list = Registry.LocalMachine.GetSubKeyNames();
    }
  }

  internal class TestTimer : EsuTimer
  {
    public TestTimer(long distance)
      : base(distance)
    {
    }

    protected override void StartProcess()
    {
      Console.WriteLine("Start process");
    }

    private int i = 0;
    protected override void Progress()
    {
      i++;
      Console.WriteLine(i.ToString(CultureInfo.InvariantCulture));
    }
  }
}
