using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lop thuc the Don vi
  /// </summary>
  [Table]
  [DisplayName("DPNDepartment")]
  public class Department : BaseEntity
  {
    /// <summary>
    /// Khoa chinh
    /// </summary>
    [PrimaryKey]
    public Guid DepartmentId { get; set; }
    /// <summary>
    /// Ma don vi
    /// </summary>
    [Column]
    [Required]
    [DisplayName("DPNDepartmentCode")]
    [RegularExpression(@"^DV-\d{1,22}$")]
    public string? DepartmentCode { get; set; }
    /// <summary>
    /// Ten don vi
    /// </summary>
    [Column]
    [Required]
    [DisplayName("DPNDepartmentName")]
    [StringLength(128)]
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Mo ta
    /// </summary>
    [Column]
    [StringLength(255)]
    public string? Description { get; set; }

  }
}
