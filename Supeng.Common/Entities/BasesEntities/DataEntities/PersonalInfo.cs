using System;
using System.ComponentModel.DataAnnotations;
using Supeng.Common.Datetimes;

namespace Supeng.Common.Entities.BasesEntities.DataEntities
{
  public class PersonalInfo : EsuInfoBase
  {
    private DateTime birthDate;
    private GenderType gender;
    private string idNumber;
    private IDType idType;
    private string name;

    #region properties

    [Display(Name = "姓名")]
    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    [Display(Name = "性别")]
    public GenderType Gender
    {
      get { return gender; }
      set
      {
        if (value == gender) return;
        gender = value;
        NotifyOfPropertyChange(() => Gender);
      }
    }

    [Display(Name = "出生日期")]
    public DateTime BirthDate
    {
      get { return birthDate; }
      set
      {
        if (value.Equals(birthDate)) return;
        birthDate = value;
        NotifyOfPropertyChange(() => BirthDate);
      }
    }

    [Display(Name = "年龄")]
    public int Age
    {
      get { return (int) birthDate.DateDiff(DateTime.Now, DateInterval.Year); }
    }

    [Display(Name = "证件类型")]
    public IDType IDType
    {
      get { return idType; }
      set
      {
        if (value == idType) return;
        idType = value;
        NotifyOfPropertyChange(() => IDType);
      }
    }

    [Display(Name = "证件编号")]
    public string IDNumber
    {
      get { return idNumber; }
      set
      {
        if (value == idNumber) return;
        idNumber = value;
        NotifyOfPropertyChange(() => IDNumber);
      }
    }

    #endregion
  }

  public enum GenderType
  {
    Male,
    Female
  }

  public enum IDType
  {
    Sfz,
    Xsz,
    Jgz
  }

  public static class GenderTypeExtensions
  {
    public static string GetName(this GenderType type)
    {
      return type == GenderType.Male ? "男" : "女";
    }
  }

  public static class IDTypeExtensions
  {
    public static string GetName(this IDType type)
    {
      switch (type)
      {
        case IDType.Sfz:
          return "身份证";
        case IDType.Jgz:
          return "军官证";
        default:
          return "学生证";
      }
    }
  }
}