using Microsoft.AspNetCore.Mvc;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Api.Controllers
{
  /// <summary>
  /// Employee api
  /// </summary>
  [Route("api/v1/[controller]")]
  [ApiController]
  public class EmployeesController : BaseController<Employee>
  {
    public EmployeesController(IEmployeeRepository repo, IEmployeeService service) : base(repo, service)
    {
    }
    /// <summary>
    /// Phan trang va tim kiem theo EmployeeCode/FullName
    /// </summary>
    /// <param name="pageSize">So ban ghi/trang</param>
    /// <param name="pageNumber">So trang</param>
    /// <param name="search">Tu khoa</param>
    /// <returns></returns>
    [HttpGet("Filter")]
    public IActionResult GetPaged(int pageSize = 10, int pageNumber = 0, string? search = null)
    {
      return Ok((Repo as IEmployeeRepository).GetPaged(pageNumber, pageSize, search));
    }
    /// <summary>
    /// Lay EmployeeCode moi
    /// </summary>
    /// <returns></returns>
    [HttpGet("NextCode")]
    public IActionResult GetNextCode()
    {
      return Ok((Service as IEmployeeService).NextCode());
    }
  }
}
