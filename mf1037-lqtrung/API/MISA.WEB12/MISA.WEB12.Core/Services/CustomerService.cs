using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Exceptions;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Validate dữ liệu riêng
        /// </summary>
        /// <param name="customer"></param>
        /// <exception cref="MISAValidateException"></exception>
        /// Created by LQTrung(26/01/2022)
        protected override void ValidateEntity(Customer customer)
        {
            //Ngày sinh không được lớn hơn ngày hiện tại:
            if(customer.DateOfBirth > DateTime.Now)
            {
                throw new MISAValidateException("Ngày sinh không được lớn hơn ngày hiện tại");
            }
        }

        public int MultiUpdateService(List<Customer> customers)
        {
            throw new NotImplementedException();
        }
    }
}
