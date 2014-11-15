using System;
using Supeng.Common.Types;

namespace Supeng.Common.Entities
{
  public struct EsuVersion
  {
    public bool Equals(EsuVersion other)
    {
      return major == other.major && minor == other.minor && build == other.build;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is EsuVersion && Equals((EsuVersion)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hashCode = major;
        hashCode = (hashCode * 397) ^ minor;
        hashCode = (hashCode * 397) ^ build;
        return hashCode;
      }
    }

    private readonly int major;
    private readonly int minor;
    private readonly int build;

    public EsuVersion(int major, int minor, int build)
    {
      this.major = major;
      this.minor = minor;
      this.build = build;
    }

    public int Major
    {
      get { return major; }
    }

    public int Minor
    {
      get { return minor; }
    }

    public int Build
    {
      get { return build; }
    }

    public static bool operator >(EsuVersion v1, EsuVersion v2)
    {
      return v1.Major * 100 + v1.Minor * 10 + v1.Build > v2.Major * 100 + v2.Minor * 10 + v2.Build;
    }

    public static bool operator ==(EsuVersion v1, EsuVersion v2)
    {
      return v1.Major * 100 + v1.Minor * 10 + v1.Build == v2.Major * 100 + v2.Minor * 10 + v2.Build;
    }

    public static bool operator !=(EsuVersion v1, EsuVersion v2)
    {
      return !(v1 == v2);
    }

    public static bool operator <(EsuVersion v1, EsuVersion v2)
    {
      return !(v1 > v2 && v1 != v2);
    }

    public static explicit operator string(EsuVersion version)
    {
      return version.ToString();
    }

    public static implicit operator EsuVersion(string value)
    {
      var data = value.Replace("V", "").Split('.');
      var major = 0;
      var minor = 0;
      var build = 0;
      try
      {
        major = data[0].ConvertData(0);
        minor = data[1].ConvertData(0);
        build = data[2].ConvertData(1);
      }
      catch
      {
        Console.WriteLine("convert error");
      }
      return new EsuVersion(major, minor, build);
    }

    public override string ToString()
    {
      return string.Format("V{0}.{1}.{2}", major, minor, build);
    }
  }
}
