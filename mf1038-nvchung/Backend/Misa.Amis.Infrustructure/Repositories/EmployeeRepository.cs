using Dapper;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using MySqlConnector;

namespace Misa.Amis.Infrastructure.Repositories
{
  public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
  {
    public EmployeeRepository(Func<MySqlConnection> getConnection) : base(getConnection)
    {
    }
    /// <summary>
    /// Phan trang va tim kiem Employee theo EmployeeCode va FullName
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public Page<Employee> GetPaged(int pageNumber, int pageSize, string? search = null)
    {
      var parameters = new DynamicParameters(new { pageNumber, pageSize });
      string? condition = null;
      //condition tim kiem theo EmployeeCode va FullName
      if (search != null)
      {
        condition = "EmployeeCode like concat('%',@search,'%') or FullName like concat('%',@search,'%')";
        parameters.Add("search", search);
      }
      return GetPaged(pageSize, pageNumber, condition, parameters);
    }
    public string GetLastTouchedCode()
    {
      var sql = "select EmployeeCode, greatest(coalesce(CreatedDate,0),coalesce(ModifiedDate,0)) lastTouchedDate from Employee order by lastTouchedDate desc limit 1";//truy van EmployeeCode moi nhat
      using var db = GetConnection();
      var res = db.QueryFirstOrDefault<(string EmployeeCode, DateTime)>(sql);
      return res.EmployeeCode;
    }
  }
}
