namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute danh dau class la mot bang trong csdl
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class TableAttribute : Attribute
  {
    public string Name { get; set; } = string.Empty;
  }
}
