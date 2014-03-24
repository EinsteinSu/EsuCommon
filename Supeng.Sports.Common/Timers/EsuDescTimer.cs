using System;
using System.IO;
using System.Threading.Tasks;

namespace Supeng.Sports.Common.Timers
{
  public class EsuDescTimer : EsuTimerBase
  {
    public EsuDescTimer(int minute, int second, TaskCreationOptions creationOptions)
      : base(0, minute, second, 1000, creationOptions)
    {
    }

    protected override DateTime TimeRun(DateTime? time)
    {
      if (time != null)
        return time.Value.AddSeconds(-1);
      throw new InvalidDataException("Time is null");
    }

    protected override bool BreakeCondition
    {
      get { return Time.TimeOfDay.TotalSeconds == 0; }
    }
  }
}