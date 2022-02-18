using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Attribute
{
    /// <summary>
    /// Không được phép trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : System.Attribute
    {

    }

    /// <summary>
    /// Tên thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : System.Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Không được phép trùng lặp
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : System.Attribute
    {

    }

    /// <summary>
    /// Khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : System.Attribute
    {

    }

    /// <summary>
    /// Thuộc tính không thêm mới
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class MISAInsertQuery : System.Attribute
    {

    }

    /// <summary>
    /// Thuộc tính bắt buộc
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : System.Attribute
    {

    }

    /// <summary>
    /// Định dạng Email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : System.Attribute
    {

    }

    /// <summary>
    /// Định dạng số
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Number : System.Attribute
    {

    }
}
