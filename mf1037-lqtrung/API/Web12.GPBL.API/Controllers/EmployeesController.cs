using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web12.GPBL.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web12.GPBL.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Khai báo thông tin CSDL:
        private string connectionString = "" +
            "Server = 47.241.69.179;" +
            "Port = 3306;" +
            "Database = MISA.CukCuk_Demo_NVMANH_copy;" +
            "User Id = dev; " +
            "Password = manhmisa";

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - danh sách nhân viên
        /// 204 - không có dữ liệu
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);

                //Thực hiện truy vấn dữ liệu trong database:
                var employees = sqlConnection.Query<Employee>("select * from Employee");
                return StatusCode(200, employees);
            }
            catch (Exception e)
            {
                //ghi log
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - 1 nhân viên 
        /// 204 - không có dữ liệu
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        // GET api/<EmployeeController>/5
        [HttpGet("{employeeId}")]
        public IActionResult Get(string employeeId)
        {
            try
            {
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Khai báo truy vấn:
                var sqlCommand = $"select * from Employee where EmployeeId = @EmployeeId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);

                //Thực hiện truy vấn dữ liệu trong database:
                var employee = sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

                return StatusCode(200, employee);
            }
            catch (Exception e)
            {
                //ghi log
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <returns>
        /// 201 - thêm mới thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                //khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);

                //Thực hiện validate dữ liệu:
                //1. Mã nhân viên không được phép để trống:
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    var response = new
                    {
                        devMsg = "Mã nhân viên không được phép để trống",
                        userMsg = "Mã nhân viên không được phép để trống",
                        data = employee
                    };
                    return StatusCode(400, response);
                }
                //2. Mã nhân viên không được phép trùng với mã nhân viên khác:
                var eCodeCheck = "select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
                var eCodeDuplicate = sqlConnection.QueryFirstOrDefault<string>(eCodeCheck, param: new { EmployeeCode = employee.EmployeeCode });
                if (eCodeDuplicate != null)
                {
                    var response = new
                    {
                        devMsg = "Mã nhân viên không được phép trùng",
                        userMsg = "Mã nhân viên không được phép trùng",
                        data = employee
                    };
                    return BadRequest(response);
                }
                //Khai báo truy vấn:
                var sqlCommand = $"insert into Employee(EmployeeId, EmployeeCode, FullName) values (@EmployeeId, @EmployeeCode, @FullName)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employee.EmployeeId);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@FullName", employee.FullName);

                //Thực hiện thêm mới:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return StatusCode(201, res);
            }
            catch (Exception e)
            {
                //ghi log
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                    data = employee
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Tìm kiếm dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - danh sách nhân viên
        /// 204 - không có dữ liệu
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        //
        [HttpGet("search")]
        public IActionResult SearchEmployees(string search)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //khai báo truy vấn:
            var sqlCommand = $"select * from Employee where FullName like CONCAT('%',@search,'%')";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@search", search);

            //thực hiện tìm kiếm:
            var res = sqlConnection.Query<Employee>(sqlCommand, param: parameters);
            return StatusCode(200, res);
        }

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - sửa dữ liệu thành công
        /// 204 - không có dữ liệu
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        // PUT api/<EmployeeController>/5
        [HttpPut("{employeeId}")]
        public IActionResult Put(string employeeId, [FromBody] Employee employee)
        {
            try
            {
                //khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);

                //thực hiện validate dữ liệu:
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    var response = new
                    {
                        devMsg = "Mã nhân viên không được phép để trống",
                        userMsg = "Mã nhân viên không được phép để trống",
                        data = employee
                    };
                    return StatusCode(400, response);
                }

                //kiểm tra employeeId có tồn tại không:
                var employeeIdCheck = $"select * from Employee where EmployeeId = @EmployeeId";
                var check1 = sqlConnection.QueryFirstOrDefault<Employee>(employeeIdCheck, param: new { EmployeeId = employeeId });
                if (check1 == null)
                {
                    return StatusCode(204, employeeId);
                }

                //kiểm tra mã nhân viên có bị trùng lặp không:
                var employeeCodeCheck = "select EmployeeCode from Employee where EmployeeCode = @EmployeeCode and EmployeeId != @EmployeeId";
                var check2 = sqlConnection.QueryFirstOrDefault<string>(employeeCodeCheck, param: new { EmployeeCode = employee.EmployeeCode, EmployeeId = employeeId });
                if (check2 != null)
                {
                    return StatusCode(400, employee);
                }

                //khai báo truy vấn:
                var sqlCommand = $"update Employee set EmployeeCode = @EmployeeCode, FullName = @FullName where EmployeeId = @EmployeeId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@FullName", employee.FullName);

                //thực hiện sửa:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return StatusCode(200, res);
            }
            catch (Exception e)
            {
                //ghi log
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                    data = employee
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - xóa dữ liệu thành công
        /// 204 - không có dữ liệu
        /// 500 - exception
        /// </returns>
        /// CreatedBy: LQTrung(13/01/2022)
        // DELETE api/<EmployeeController>/5
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(string employeeId)
        {
            try
            {
                //khởi tạo kết nối:
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //kiểm tra id:
                var employeeIdCheck = "select * from Employee where EmployeeId = @EmployeeId";
                var check = sqlConnection.QueryFirstOrDefault<Employee>(employeeIdCheck, param: new { EmployeeId = employeeId });
                if (check == null)
                {
                    return StatusCode(204, employeeId);
                }

                //khai báo truy vấn:
                var sqlCommand = $"delete from Employee where EmployeeId = @EmployeeId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);

                //thực hiện xóa:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return StatusCode(200, res);
            }
            catch (Exception e)
            {
                //ghi log
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                    data = employeeId
                };
                return StatusCode(500, response);
            }
        }
    }
}
