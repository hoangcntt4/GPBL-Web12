using MISA.WEB12.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Entities
{
    public class Department
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [NotEmpty]
        [Unique]
        [PropertyName("Mã đơn vị")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên đơn vị")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [PropertyName("Mô tả")]
        public string? Description { get; set; }

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

    }
}
