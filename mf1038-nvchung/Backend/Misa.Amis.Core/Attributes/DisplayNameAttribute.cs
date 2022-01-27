namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute chua thong tin ten hien thi cua property/class
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
  public class DisplayNameAttribute : Attribute
  {
    public string Name { get; private set; }

    public DisplayNameAttribute(string name)
    {
      Name = name;
    }
  }
}
