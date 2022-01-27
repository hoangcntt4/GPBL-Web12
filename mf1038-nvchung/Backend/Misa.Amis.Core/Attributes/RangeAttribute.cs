namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute validate du lieu nam trong khoang min-max
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class RangeAttribute : ValidationAttribute
  {
    public object? Min { get; set; }
    public object? Max { get; set; }
    public override string ErrorMessageFormat { get; set; } = "Thông tin {0} phải nằm trong khoảng {1}-{2}";

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, Min, Max);
    }

    public override bool IsValid(object? value)
    {
      if (value is not IComparable valueIC) return true;
      bool isValid = true;
      if (Min is IComparable minIC)
      {
        isValid = valueIC.CompareTo(minIC) >= 0;
      }
      if (Max is IComparable maxIC)
      {
        isValid = valueIC.CompareTo(maxIC) <= 0;
      }
      return isValid;
    }
  }
}
