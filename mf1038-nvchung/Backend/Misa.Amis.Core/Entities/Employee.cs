using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Utils;
using System.Text.Json.Serialization;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lop thuc the Nhan vien
  /// </summary>
  [Table]
  [DisplayName("Nhân viên")]
  public class Employee : BaseEntity
  {
    /// <summary>
    /// Khoa chinh
    /// </summary>
    [PrimaryKey]
    public Guid EmployeeId { get; set; }
    /// <summary>
    /// Ma nhan vien
    /// </summary>
    [Column]
    [DisplayName("Mã nhân viên")]
    [Required]
    public string? EmployeeCode { get; set; }
    /// <summary>
    /// Ho ten nhan vien
    /// </summary>
    [Column]
    [DisplayName("Họ tên nhân viên")]
    [Required]
    public string? FullName { get; set; }
    /// <summary>
    /// Gioi tinh
    /// </summary>
    [Column]
    [Range(Min = 0, Max = 2)]
    public int? Gender { get; set; }
    /// <summary>
    /// Ngay sinh
    /// </summary>
    [Column]
    [DisplayName("Ngày sinh")]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// So dien thoai di dong
    /// </summary>
    [Column]
    [DisplayName("Số điện thoại di động")]
    [PhoneNumber]
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// So dien thoai co dinh
    /// </summary>
    [Column]
    [DisplayName("Số điện thoại cố định")]
    [PhoneNumber]
    public string? TelephoneNumber { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [Column]
    [DisplayName("Email")]
    [Email]
    public string? Email { get; set; }
    /// <summary>
    /// Dia chi
    /// </summary>
    [Column]
    public string? Address { get; set; }
    /// <summary>
    /// So CMND
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("Số CMND")]
    public string? IdentityNumber { get; set; }
    /// <summary>
    /// Noi cap
    /// </summary>
    [Column]
    public string? IdentityPlace { get; set; }
    /// <summary>
    /// Ngay cap
    /// </summary>
    [Column]
    [DisplayName("Ngày cấp")]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? IdentityDate { get; set; }
    /// <summary>
    /// Ma don vi
    /// </summary>
    [Column]
    [DisplayName("Mã đơn vị")]
    [Required]
    public Guid? DepartmentId { get; set; }
    /// <summary>
    /// Chuc vu
    /// </summary>
    [Column]
    public string? EmployeePosition { get; set; }
    /// <summary>
    /// So tai khoan
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("Số tài khoản")]
    public string? BankAccountNumber { get; set; }
    /// <summary>
    /// Ten ngan hang
    /// </summary>
    [Column]
    public string? BankName { get; set; }
    /// <summary>
    /// Ten chi nhanh
    /// </summary>
    [Column]
    public string? BankBranchName { get; set; }
    /// <summary>
    /// Ten don vi
    /// </summary>
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Ma don vi
    /// </summary>
    public string? DepartmentCode { get; set; }
  }
}
