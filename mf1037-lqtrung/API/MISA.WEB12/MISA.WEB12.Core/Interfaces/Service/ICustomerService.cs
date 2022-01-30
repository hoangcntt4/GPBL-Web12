using MISA.WEB12.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Interfaces.Service
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>Số bản ghi đã cập nhật</returns>
        int MultiUpdateService(List<Customer> customers);
    }
}
