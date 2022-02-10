namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute validate email
  /// </summary>
  public class EmailAttribute : RegularExpressionAttribute
  {
    public EmailAttribute() : base(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
    {
      ErrorMessageFormat = Resources.Text.VEMEmail;
    }
  }
}
