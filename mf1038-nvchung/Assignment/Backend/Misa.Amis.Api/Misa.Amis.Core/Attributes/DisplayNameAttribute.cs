namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute chua thong tin ten hien thi cua property/class
  /// </summary>
  [AttributeUsage(AttributeTargets.All)]
  public class DisplayNameAttribute : Attribute
  {
    string _name;
    public string Name { get => Resources.Text.ResourceManager.GetString(_name); }

    public DisplayNameAttribute(string name)
    {
      _name = name;
    }
  }
}
