using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lop thuc the Don vi
  /// </summary>
  [Table]
  [DisplayName("Đơn vị")]
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
    [DisplayName("Mã đơn vị")]
    public string? DepartmentCode { get; set; }
    /// <summary>
    /// Ten don vi
    /// </summary>
    [Column]
    [Required]
    [DisplayName("Tên đơn vị")]
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Mo ta
    /// </summary>
    [Column]
    public string? Description { get; set; }

  }
}
