using MISA.WEB12.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Entities
{
    public class Customer
    {
        #region Contrucstor
        public Customer()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã khách hàng")]
        [Unique]
        public string CustomerCode { get; set; }
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
        [PropertyName("họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [NotEmpty]
        [PropertyName("Số điện thoại")]
        public string PhoneNumber { get; set; }
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
