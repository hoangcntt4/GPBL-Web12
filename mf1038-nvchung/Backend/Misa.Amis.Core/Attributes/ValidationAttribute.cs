namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Lop truu tuong validate du lieu
  /// </summary>
  public abstract class ValidationAttribute : Attribute
  {
    /// <summary>
    /// Chuoi format loi trong <see cref="string.Format(string, object?)"/>
    /// </summary>
    public abstract string ErrorMessageFormat { get; set; }
    /// <summary>
    /// Method format chuoi loi
    /// </summary>
    /// <param name="displayName">Ten hien thi cua property</param>
    /// <returns></returns>
    public abstract string FormatErrorMessage(string displayName);
    /// <summary>
    /// Method kiem tra hop le du lieu
    /// </summary>
    /// <param name="value">Gia tri can validate</param>
    /// <returns>True neu hop le</returns>
    public abstract bool IsValid(object? value);
  }
}
