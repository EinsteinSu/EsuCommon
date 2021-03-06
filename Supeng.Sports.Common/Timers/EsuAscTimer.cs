﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Supeng.Sports.Common.Timers
{
  public sealed class EsuAscTimer : EsuTimerBase
  {
    public EsuAscTimer(TaskCreationOptions creationOptions)
      : base(0, 0, 0, 1000, creationOptions)
    {
      DateTime time = Time;
    }

    protected override bool BreakeCondition
    {
      get { return false; }
    }

    protected override DateTime TimeRun(DateTime? time)
    {
      if (time != null)
        return time.Value.AddSeconds(1);
      throw new InvalidDataException("Time is null");
    }
  }
}