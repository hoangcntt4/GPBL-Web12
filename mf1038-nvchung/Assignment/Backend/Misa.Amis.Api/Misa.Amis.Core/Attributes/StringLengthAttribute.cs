using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Amis.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class StringLengthAttribute : ValidationAttribute
  {
    public int MaxLength { get; private set; }
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMStringLength;

    public StringLengthAttribute(int maxLength)
    {
      MaxLength = maxLength;
    }

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, MaxLength);
    }

    public override bool IsValid(object? value)
    {
      if (value == null || value is not string val)
      {
        return true;
      }
      return val.Length <= MaxLength;
    }
  }
}
