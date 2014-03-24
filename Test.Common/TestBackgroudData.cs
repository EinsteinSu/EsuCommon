﻿using System;
using NUnit.Framework;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

namespace Test.Common
{
  [TestFixture, RequiresSTA]
  public class TestBackgroudData : IBackgroundData<int>, IExceptionHandle
  {
    private readonly int assertResult;

    public TestBackgroudData(int assertResult)
    {
      this.assertResult = assertResult;
    }

    [Test, RequiresSTA]
    public void BeginExecute()
    {
      Console.WriteLine("Begin execute");
    }

    [Test, RequiresSTA]
    public void EndExecute(int result)
    {
      Assert.AreEqual(result, assertResult);
      Console.WriteLine("Execute success the effect result has {0} records.", result);
    }

    public void CancelExecute()
    {
      Console.WriteLine("Execute has canceled.");
    }

    public void HandleBackgroundException(Exception[] exceptions)
    {
      foreach (var exception in exceptions)
      {
        Console.WriteLine(exception.Message);
      }
    }

    public void Handle(Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}
