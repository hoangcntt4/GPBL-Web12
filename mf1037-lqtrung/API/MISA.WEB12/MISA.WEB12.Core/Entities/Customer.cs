using MISA.WEB12.Core.Attribute;
using MISA.WEB12.Core.Enum;
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
        [PropertyName("họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [PropertyName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [NotEmpty]
        [Number]
        [PropertyName("Số điện thoại cố định")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [PropertyName("Địa chỉ Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [PropertyName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

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

        /// <summary>
        /// Giới tính
        /// </summary>
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
        #endregion
    }
}
