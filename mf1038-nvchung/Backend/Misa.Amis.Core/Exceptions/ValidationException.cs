namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception validate du lieu
  /// </summary>
  public class ValidationException : Exception
  {
    public ValidationException(string message, string? errPropName = null) : base(message)
    {
      Data["ErrorProperty"] = errPropName;
    }
  }
}
