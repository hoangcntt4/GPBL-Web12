using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;

namespace MISA.WEB12.Api.Controllers
{
    public class DepartmentsController : MISABaseController<Department>
    {
        #region Constructor
        public DepartmentsController(IBaseService<Department> baseService, IBaseRepository<Department> baseRepository) : base(baseService, baseRepository)
        {

        }
        #endregion
    }
}
