using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Entities;
using System.Reflection;

namespace Misa.Amis.Core.Utils
{
  /// <summary>
  /// Lop tien ich cho Entity
  /// </summary>
  public static class EntityUtils
  {
    /// <summary>
    /// Lay ra ten bang ma lop thuc the dai dien
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception">Loi thuc the khong duoc danh dau voi TableAttribute</exception>
    public static string GetTableName<T>() where T : BaseEntity
    {
      var type = typeof(T);
      if (Attribute.GetCustomAttribute(type, typeof(TableAttribute)) is TableAttribute tbAttr)
      {
        return string.IsNullOrEmpty(tbAttr.Name) ? type.Name : tbAttr.Name;
      }
      throw new Exception($"{type.Name} must be annotated with TableAttribute");
    }
    /// <summary>
    /// Lay ra ten cac cot
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="includePrimaryKey">Co bao gom cot khoa chinh khong?</param>
    /// <returns></returns>
    public static IEnumerable<string> GetColumnNames<T>(bool includePrimaryKey = true) where T : BaseEntity
    {
      return typeof(T).GetProperties().Where(p => p.IsDefined(typeof(ColumnAttribute)) || (includePrimaryKey && p.IsDefined(typeof(PrimaryKeyAttribute)))).Select(p => p.Name);
    }
    /// <summary>
    /// Lay ra property khoa chinh
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns></returns>
    public static PropertyInfo GetPrimaryKeyProperty<T>() where T : BaseEntity
    {
      return typeof(T).GetProperties().First(p => p.IsDefined(typeof(PrimaryKeyAttribute)));
    }
    /// <summary>
    /// Lay ra ten hien thi
    /// </summary>
    /// <param name="member">Property/Class</param>
    /// <returns></returns>
    public static string GetDisplayName(MemberInfo member)
    {
      var dpNameAttr = member.GetCustomAttribute<DisplayNameAttribute>();
      return dpNameAttr?.Name ?? member.Name;
    }
  }
}
