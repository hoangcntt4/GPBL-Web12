using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Exceptions;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;

namespace MISA.WEB12.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : MISABaseController<Customer>
    {
        ICustomerRepository _customerRepository;
        ICustomerService _customerServices;
        public CustomersController(ICustomerRepository customerRepository, ICustomerService customerService):base(customerService, customerRepository)
        {
            _customerRepository = customerRepository;
            _customerServices = customerService;
        }
    }
}
