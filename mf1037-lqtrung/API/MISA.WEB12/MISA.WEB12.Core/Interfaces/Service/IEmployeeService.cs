using MISA.WEB12.Core.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Interfaces.Service
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <returns></returns>
        /// Created by LQTrung(13/02/2022)
        ExcelPackage ExportExcel();
    }
}
