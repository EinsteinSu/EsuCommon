using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Supeng.Common.Entities;

namespace Supeng.Sports.Common.Timers
{
  public abstract class EsuTimerBase : EsuInfoBase
  {
    private int hour;
    private int minute;
    private int second;
    private readonly int interval;
    private readonly TaskCreationOptions creationOptions;
    private string displayFormat;
    private CancellationTokenSource cancellationTokenSource;
    private DateTime? time;

    protected EsuTimerBase(int hour, int minute, int second, int interval, TaskCreationOptions creationOptions)
    {
      this.hour = hour;
      this.minute = minute;
      this.second = second;
      this.interval = interval;
      this.creationOptions = creationOptions;
      displayFormat = "mm:ss";
    }

    #region time properties
    [Display(AutoGenerateField = false)]
    public int Hour
    {
      get { return hour; }
    }

    [Display(AutoGenerateField = false)]
    public int Minute
    {
      get { return minute; }
    }

    [Display(AutoGenerateField = false)]
    public int Second
    {
      get { return second; }
    }

    [Display(AutoGenerateField = false)]
    protected virtual DateTime Time
    {
      get
      {
        if (!time.HasValue)
          time = new DateTime(2014, 1, 1, hour, minute, second);
        return time.Value;
      }
    }

    [Display(AutoGenerateField = false)]
    public string DisplayTime
    {
      get { return Time.ToString(DisplayFormat); }
    }
    #endregion

    [Display(AutoGenerateField = false)]
    public string DisplayFormat
    {
      get { return displayFormat; }
      set
      {
        if (value == displayFormat) return;
        displayFormat = value;
        NotifyOfPropertyChange(() => DisplayFormat);
      }
    }

    public void SetTime(int h, int m, int s)
    {
      time = null;
      hour = h;
      minute = m;
      second = s;
    }

    protected abstract DateTime TimeRun(DateTime? time);

    protected abstract bool BreakeCondition { get; }

    protected virtual void BeforeStart()
    {

    }

    protected virtual void TimeRuning(DateTime? dateTime)
    {

    }

    public void Start()
    {
      cancellationTokenSource = new CancellationTokenSource();
      BeforeStart();
      var task = new Task(() =>
      {
        while (true)
        {
          lock (this)
          {
            if (cancellationTokenSource.IsCancellationRequested || BreakeCondition)
              break;
            time = TimeRun(time);
            TimeRuning(time);
            NotifyOfPropertyChange(() => Time);
            NotifyOfPropertyChange(() => DisplayTime);
            Thread.Sleep(interval);
          }
        }
      }, cancellationTokenSource.Token, creationOptions);
      task.Start();
    }

    protected virtual void Stoped()
    {

    }

    public void Stop()
    {
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
        Stoped();
      }
    }

    protected virtual void Reseted()
    {

    }

    public void Reset()
    {
      if (cancellationTokenSource != null)
        cancellationTokenSource.Cancel();
      time = null;
      NotifyOfPropertyChange(() => Time);
      NotifyOfPropertyChange(() => DisplayTime);
      Reseted();
    }
  }
}