using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Exceptions;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.WEB12.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        #region Fields
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeService, employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tìm kiếm + phân trang
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// Created By LQTrung(25/01/2022)
        [HttpGet("paging")]
        public IActionResult GetPaging(string? searchText, int pageIndex, int pageSize)
        {
            try
            {
                var data = _employeeRepository.GetPaging(searchText, pageIndex, pageSize);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        /// Created By LQTrung(07/02/2022)
        [HttpPost("delete-many")]
        public IActionResult DeleteMany(Guid[] employeeIds)
        {
            try
            {
                var data = _employeeRepository.DeleteMany(employeeIds);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Lấy mã nhân viên tự tăng
        /// </summary>
        /// <returns></returns>
        /// Created By LQTrung(07/02/2022)

        [HttpGet("next-code")]
        public IActionResult GetNextCode()
        {
            try
            {
                var data = _employeeRepository.GetNextCode();
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Xuất danh sách nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// Created By LQTrung(13/02/2022)

        [HttpGet("export")]
        public async Task<IActionResult> GetEmployeesExcel()
        {
            await Task.Yield();
            var res = _employeeService.ExportExcel();
            var stream = new MemoryStream();
            res.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danh-sach-nhan-vien.xlsx");
        }
        #endregion
    }
}
