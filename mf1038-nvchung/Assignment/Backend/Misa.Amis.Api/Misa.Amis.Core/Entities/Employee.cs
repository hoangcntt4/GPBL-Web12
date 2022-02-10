using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Enums;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lop thuc the Nhan vien
  /// </summary>
  [Table]
  [DisplayName("DPNEmployee")]
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
    [DisplayName("DPEmployeeCode")]
    [Required]
    [RegularExpression(@"^NV-\d{1,22}$")]
    public string? EmployeeCode { get; set; }
    /// <summary>
    /// Ho ten nhan vien
    /// </summary>
    [Column]
    [DisplayName("DPNFullName")]
    [Required]
    [StringLength(200)]
    public string? FullName { get; set; }
    /// <summary>
    /// Gioi tinh
    /// </summary>
    [Column]
    [DisplayName("DPNGender")]
    public Gender Gender { get; set; }
    /// <summary>
    /// Ngay sinh
    /// </summary>
    [Column]
    [DisplayName("DPNDateOfBirth")]
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// So dien thoai di dong
    /// </summary>
    [Column]
    [DisplayName("DPNPhoneNumber")]
    [PhoneNumber]
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// So dien thoai co dinh
    /// </summary>
    [Column]
    [DisplayName("DPNTelephoneNumber")]
    [PhoneNumber]
    public string? TelephoneNumber { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [Column]
    [DisplayName("DPNEmail")]
    [Email]
    [StringLength(255)]
    public string? Email { get; set; }
    /// <summary>
    /// Dia chi
    /// </summary>
    [Column]
    [DisplayName("DPNAddress")]
    [StringLength(255)]
    public string? Address { get; set; }
    /// <summary>
    /// So CMND
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("DPNIndetityNumber")]
    [StringLength(20)]
    public string? IdentityNumber { get; set; }
    /// <summary>
    /// Noi cap
    /// </summary>
    [Column]
    [DisplayName("DPNIndentityPlace")]
    [StringLength(255)]
    public string? IdentityPlace { get; set; }
    /// <summary>
    /// Ngay cap
    /// </summary>
    [Column]
    [DisplayName("DPNIndetityDate")]
    public DateTime? IdentityDate { get; set; }
    /// <summary>
    /// Ma don vi
    /// </summary>
    [Column]
    [Required]
    public Guid? DepartmentId { get; set; }
    /// <summary>
    /// Chuc vu
    /// </summary>
    [Column]
    [DisplayName("DPNEmployeePosition")]
    [StringLength(128)]
    public string? EmployeePosition { get; set; }
    /// <summary>
    /// So tai khoan
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("DPNBankAccountNumber")]
    [StringLength(50)]
    public string? BankAccountNumber { get; set; }
    /// <summary>
    /// Ten ngan hang
    /// </summary>
    [Column]
    [DisplayName("DPNBankName")]
    [StringLength(120)]
    public string? BankName { get; set; }
    /// <summary>
    /// Ten chi nhanh
    /// </summary>
    [Column]
    [DisplayName("DPNBankBranchName")]
    [StringLength(255)]
    public string? BankBranchName { get; set; }
    /// <summary>
    /// Ten don vi
    /// </summary>
    /// 
    [DisplayName("DPNDepartmentName")]
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Ma don vi
    /// </summary>
    /// 
    [DisplayName("DPNDepartmentCode")]
    public string? DepartmentCode { get; set; }
  }
}
