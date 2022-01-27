using System.Text.RegularExpressions;

namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute validate du lieu theo bieu thuc chinh quy
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class RegularExpressionAttribute : ValidationAttribute
  {
    public string Pattern { get; private set; }

    public RegularExpressionAttribute(string pattern)
    {
      Pattern = pattern;
    }

    public override string ErrorMessageFormat { get; set; } = "Thông tin {0} không khớp định dạng {1}";

    public override bool IsValid(object? value)
    {
      if (value is string s && !string.IsNullOrEmpty(s))
      {
        return Regex.IsMatch(s, Pattern);
      }
      return true;
    }

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, Pattern);
    }
  }
}
