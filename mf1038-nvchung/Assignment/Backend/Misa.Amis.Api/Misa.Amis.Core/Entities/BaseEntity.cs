using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lop Entity base
  /// </summary>
  public class BaseEntity
  {
    /// <summary>
    /// Ngay tao
    /// </summary>
    [Column]
    [DisplayName("DPNCreatedBy")]
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Ngay sua
    /// </summary>
    [Column]
    [DisplayName("DPNModifiedBy")]
    public string? ModifiedBy { get; set; }
    /// <summary>
    /// Nguoi tao
    /// </summary>
    [Column]
    [DisplayName("DPNCreatedDate")]
    public DateTime? CreatedDate { get; set; }
    /// <summary>
    /// Nguoi sua
    /// </summary>
    [Column]
    [DisplayName("DPNModifiedDate")]
    public DateTime? ModifiedDate { get; set; }
  }
}
