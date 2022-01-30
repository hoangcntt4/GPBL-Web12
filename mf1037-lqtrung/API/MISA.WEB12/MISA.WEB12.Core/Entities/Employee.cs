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
        public string? FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
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

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? EmployeePosition { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [MISAInserQuery]
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
        #endregion
    }
}
