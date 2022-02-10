namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute validate so dien thoai
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class PhoneNumberAttribute : RegularExpressionAttribute
  {
    public PhoneNumberAttribute() : base(@"^\d{10,12}$")
    {
      ErrorMessageFormat = Resources.Text.VEMPhoneNumber;
    }
  }
}
