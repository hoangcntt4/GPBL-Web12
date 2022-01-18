using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web12.GPBL.API.Models
{
    /// <summary>
    /// Thông tin nhân viên
    /// CreatedBy: LQTrung (12/1/2022)
    /// </summary>
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
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }
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
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        #endregion

    }
}
