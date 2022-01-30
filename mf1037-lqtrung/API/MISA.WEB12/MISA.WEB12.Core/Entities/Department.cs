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
        [NotEmpty]
        [Unique]
        [PropertyName("Mã đơn vị")]
        public string DepartmentCode { get; set; }

        [NotEmpty]
        public string DepartmentName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifieldDate { get; set; } 
        public string? ModifieldBy { get; set; }

    }
}
