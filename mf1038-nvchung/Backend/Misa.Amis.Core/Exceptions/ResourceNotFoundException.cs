namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception khong tim thay resource
  /// </summary>
  public class ResourceNotFoundException : Exception
  {
    public ResourceNotFoundException(string resourceName) : base($"Không tìm thấy {resourceName}") { }
  }
}
