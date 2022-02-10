namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute danh dau property la bat buoc
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class RequiredAttribute : ValidationAttribute
  {
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMRequired;

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName);
    }

    public override bool IsValid(object? value)
    {
      if (value is string s)
      {
        return !string.IsNullOrEmpty(s);
      }
      return value != null;
    }
  }
}
