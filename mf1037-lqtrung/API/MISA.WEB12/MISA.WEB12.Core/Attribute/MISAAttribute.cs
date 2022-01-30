using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : System.Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : System.Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public  class MISAInserQuery : System.Attribute
    {

    }
}
