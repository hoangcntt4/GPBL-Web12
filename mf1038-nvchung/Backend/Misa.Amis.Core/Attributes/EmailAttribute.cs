namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute validate email
  /// </summary>
  public class EmailAttribute : RegularExpressionAttribute
  {
    public EmailAttribute() : base(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
    {
      ErrorMessageFormat = "Thông tin {0} không đúng định dạng email";
    }
  }
}
