using System;
using Supeng.Common.Datetimes;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.Models
{
  public class DateTimeSearchModel : EsuInfoBase, ISearchModel
  {
    private readonly DateTime originalDate;
    private readonly string title;
    protected string ColumnName;
    private DateTime endDate;
    private DateTime startDate;

    public DateTimeSearchModel()
    {
    }

    public DateTimeSearchModel(string title, string columnName, DateTime startDate, DateTime endDate)
    {
      this.title = title;
      ColumnName = columnName;
      this.startDate = startDate;
      this.endDate = endDate;
      originalDate = startDate;
    }

    public DateTimeSearchModel(string title, string columnName, DateInterval interval = DateInterval.Month)
    {
      this.title = title;
      ColumnName = columnName;
      endDate = DateTime.Now;
      switch (interval)
      {
        case DateInterval.Day:
          startDate = DateTime.Now.AddDays(-1);
          break;
        case DateInterval.Week:
          startDate = DateTime.Now.AddDays(-7);
          break;
        case DateInterval.Month:
          startDate = DateTime.Now.AddMonths(-1);
          break;
        case DateInterval.Quarter:
          startDate = DateTime.Now.AddMonths(-3);
          break;
        case DateInterval.Year:
          startDate = DateTime.Now.AddYears(-1);
          break;
      }
      originalDate = startDate;
    }

    public virtual string DateTitle
    {
      get { return title; }
    }

    public DateTime StartDate
    {
      get { return startDate; }
      set
      {
        if (value.Equals(startDate)) return;
        startDate = value;
        NotifyOfPropertyChange(() => StartDate);
      }
    }

    public DateTime EndDate
    {
      get { return endDate; }
      set
      {
        if (value.Equals(endDate)) return;
        endDate = value;
        NotifyOfPropertyChange(() => EndDate);
      }
    }

    public virtual string Search()
    {
      return startDate.GetBetweenSqlScript(endDate, ColumnName);
    }

    public virtual void Clear()
    {
      StartDate = originalDate;
      EndDate = DateTime.Now;
    }
  }
}