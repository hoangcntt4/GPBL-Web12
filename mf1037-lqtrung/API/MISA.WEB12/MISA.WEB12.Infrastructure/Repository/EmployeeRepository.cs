using Dapper;
using MISA.WEB12.Core.Entities;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Methods
        public object GetPaging(string? searchText, int pageIndex, int pageSize)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                if (searchText == null)
                {
                    searchText = "";
                }
                //Khai báo truy vấn:
                var sqlCommand = "Proc_PagingEmployees";
                Parameters = new DynamicParameters();
                Parameters.Add("@m_SearchText", searchText);
                Parameters.Add("@m_PageIndex", pageIndex);
                Parameters.Add("@m_PageSize", pageSize);
                Parameters.Add("@m_TotalRecord", direction: System.Data.ParameterDirection.Output);

                //Thực hiện truy vấn:
                var entities = sqlConnection.Query<object>(sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
                var totalRecord = Parameters.Get<int>("@m_TotalRecord");
                return new
                {
                    TotalRecord = totalRecord,
                    Data = entities,
                };
            }
        }
        public int DeleteMany(Guid[] employeeIds)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Khai báo truy vấn:
                var sqlCommand = "delete from Employee where EmployeeId in @employeeIds";

                //Thực hiện xóa:
                var res = sqlConnection.Execute(sqlCommand, new { employeeIds });
                return res;
            }
        }

       
        public string GetNextCode()
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Khai báo truy vấn:
                var sqlCommand = "select max(cast(substr(EmployeeCode,4) as int)) from Employee";

                //Thực hiện truy vấn:
                var res = sqlConnection.QueryFirstOrDefault<int>(sqlCommand)+1;
                var nextCode = res < 10 ? $"NV-000{res}" : res < 100 ? $"NV-00{res}" : res < 1000 ? $"NV-0{res}" : $"NV-{res}";
                return nextCode;
            }
        }
        #endregion
    }
}
