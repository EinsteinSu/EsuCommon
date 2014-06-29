using System;

namespace Supeng.Common.Datetimes
{
  public static class DateTimeExtensions
  {
    public static long DateDiff(this DateTime startDate, DateTime endDate, DateInterval interval = DateInterval.Day)
    {
      long lngDateDiffValue = 0;
      var timeSpan = new TimeSpan(endDate.Ticks - startDate.Ticks);
      switch (interval)
      {
        case DateInterval.Second:
          lngDateDiffValue = (long) timeSpan.TotalSeconds;
          break;
        case DateInterval.Minute:
          lngDateDiffValue = (long) timeSpan.TotalMinutes;
          break;
        case DateInterval.Hour:
          lngDateDiffValue = (long) timeSpan.TotalHours;
          break;
        case DateInterval.Day:
          lngDateDiffValue = timeSpan.Days;
          break;
        case DateInterval.Week:
          lngDateDiffValue = timeSpan.Days/7;
          break;
        case DateInterval.Month:
          lngDateDiffValue = timeSpan.Days/30;
          break;
        case DateInterval.Quarter:
          lngDateDiffValue = (timeSpan.Days/30)/3;
          break;
        case DateInterval.Year:
          lngDateDiffValue = timeSpan.Days/365;
          break;
      }
      return (lngDateDiffValue);
    }

    public static DateTime GetIntervalForWorkDay(this DateTime startDate, long interval)
    {
      DateTime result = startDate;
      long i = 0;
      while (true)
      {
        if (i == interval)
          break;
        startDate = startDate.AddDays(1);
        if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
          continue;
        result = startDate;
        i++;
      }
      return result;
    }

    public static string GetHrDisplayTime(this DateTime time, string format = "d")
    {
      if (time == new DateTime(1900, 1, 1))
        return string.Empty;
      return time.ToString(format);
    }

    public static DateTime GetTimeValue(this DateTime? time)
    {
      return time.HasValue ? time.Value : DateTime.Parse("1900");
    }
  }

  public enum DateInterval
  {
    Second,
    Minute,
    Hour,
    Day,
    Week,
    Month,
    Quarter,
    Year
  }
}