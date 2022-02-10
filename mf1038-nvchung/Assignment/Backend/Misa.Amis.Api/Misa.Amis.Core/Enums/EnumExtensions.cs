using Misa.Amis.Core.Attributes;
using System.Reflection;

namespace Misa.Amis.Core.Enums
{
  public static class EnumExtensions
  {
    public static string GetDisplayName(this Enum val)
    {
      var f = val.GetType().GetField(val.ToString());
      if (f != null && f.IsDefined(typeof(DisplayNameAttribute)))
      {
        return f.GetCustomAttribute<DisplayNameAttribute>().Name;
      }
      return string.Empty;
    }
  }
}
