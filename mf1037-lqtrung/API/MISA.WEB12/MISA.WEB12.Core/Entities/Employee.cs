using MISA.WEB12.Core.Attribute;
using MISA.WEB12.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Entities
{
    public class Employee
    {
        #region Contrucstor
        public Employee()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã nhân viên")]
        [Unique]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        [PropertyName("Họ")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        [PropertyName("Tên")]
        public string? LastName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [NotEmpty]
        [PropertyName("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        
        public Gender? Gender { get; set; }
        [PropertyName("Giới tính")]
        [MISAInsertQuery]
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Female:
                        return Core.Resources.MISAResourceVN.Gender_Female;
                    case Enum.Gender.Male:
                        return Core.Resources.MISAResourceVN.Gender_Male;
                    case Enum.Gender.Other:
                        return Core.Resources.MISAResourceVN.Gender_Other;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [PropertyName("Điện thoại cố định")]
        [Number]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [PropertyName("Địa chỉ Email")]
        [Email]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [PropertyName("Địa chỉ")]
        public string? Address { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [PropertyName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [PropertyName("Mã đơn vị")]
        [NotEmpty]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        /// 
        [PropertyName("Chức vụ")]
        public string? PositionName{ get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        [PropertyName("Số CMND")]
        [Number]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        [PropertyName("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [PropertyName("Nơi cấp")]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        [PropertyName("Điện thoại di động")]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [PropertyName("Số tài khoản ngân hàng")]
        [Number]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [PropertyName("Tên ngân hàng")]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [PropertyName("Chi nhánh ngân hàng")]
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Thời gian tạo mới
        /// </summary>
        [PropertyName("Thời gian tạo mới")]
        public DateTime? CreatedDate => DateTime.Now;

        /// <summary>
        /// Người tạo
        /// </summary>
        [PropertyName("Người tạo")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian chỉnh sửa
        /// </summary>
        [PropertyName("Thời gian chỉnh sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        [PropertyName("Người chỉnh sửa")]
        public string? ModifiedBy { get; set; }

        #endregion
    }
}
