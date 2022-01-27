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
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Ngay sua
    /// </summary>
    [Column]
    public string? ModifiedBy { get; set; }
    /// <summary>
    /// Nguoi tao
    /// </summary>
    [Column]
    public DateTime? CreatedDate { get; set; }
    /// <summary>
    /// Nguoi sua
    /// </summary>
    [Column]
    public DateTime? ModifiedDate { get; set; }
  }
}
