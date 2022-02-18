using MISA.WEB12.Core.Attribute;
using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Exceptions;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region  Fields
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate dữ liệu riêng 
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="MISAValidateException"></exception>
        /// Created by LQTrung(26/01/2022)
        protected override void ValidateEntity(Employee employee)
        {
            //Ngày sinh không được lớn hơn ngày hiện tại:
            if (employee.DateOfBirth > DateTime.Now)
            {
                throw new MISAValidateException(Core.Resources.MISAResourceVN.ErrorDoB);
            }
            //Ngày cấp không được lớn hơn ngày hiện tại:
            if (employee.IdentityDate > DateTime.Now)
            {
                throw new MISAValidateException(Core.Resources.MISAResourceVN.ErrorIdentityDate);
            }
        }

        /// <summary>
        /// Xuất danh sách nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// Created by LQTrung(13/02/2022)
        public ExcelPackage ExportExcel()
        {
            var package = new ExcelPackage();
            //khởi tạo worksheet danh sách nhân viên
            var worksheet = package.Workbook.Worksheets.Add("ListEmployees");
            //header cột số thứ tự
            worksheet.Cells[1, 1].Value = "STT";
            //lấy ra danh sách các nhân viên
            var list = _employeeRepository.GetAll().ToList();
            //lấy ra các properties của Employee
            var props = typeof(Employee).GetProperties();

            //Loại bỏ các properties được truyền qua sets
            HashSet<string> excludedProps = new HashSet<string>() { "EmployeeId", "DepartmentId", "FirstName", "LastName", "Gender", "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy" };
            props = props.ToHashSet().ExceptBy(excludedProps, prop => prop.Name).ToArray();

            for (int i = 0; i < props.Length; i++)
            {
                //lấy tên header các cột theo tên thuộc tính 
                worksheet.Cells[1, i + 2].Value = GetDisplayName(props[i]);
                //độ dài của các cột
            }

            var th = worksheet.Cells[1, 1, 1, props.Length+1];
            th.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //màu nền của header
            th.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            //căn giữa
            th.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //in đậm
            th.Style.Font.Bold = true;

            for (int i = 0; i < list.Count(); i++)
            {
                //thứ tự trong cột STT
                worksheet.Cells[i + 2, 1].Value = (i + 1).ToString();
                for (int j = 0; j < props.Length; j++)
                {
                    //gán giá trị cho các cột tương ứng
                    var cell = worksheet.Cells[i + 2, j + 2];
                    var value = props[j].GetValue(list[i]);
                    switch (value)
                    {
                        case DateTime:
                            //định dạng cột ngày tháng
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        default:
                            break;
                    }
                    cell.Value = value;
                }
            }
            worksheet.Cells.AutoFitColumns();
            return package;
        }
        #endregion
    }
}
