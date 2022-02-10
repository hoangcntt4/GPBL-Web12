namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception khong tim thay resource
  /// </summary>
  public class ResourceNotFoundException : Exception
  {
    public ResourceNotFoundException(string resourceName) : base(string.Format(Resources.Text.EMResourceNotFound, resourceName)) { }
  }
}
