using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Enums
{
  public enum Gender
  {
    [DisplayName("DPGenderMale")]
    Male = 1,
    [DisplayName("DPGenderFemale")]
    Female = 0,
    [DisplayName("DPGenderUnknown")]
    Unknown = 2
  }
}
