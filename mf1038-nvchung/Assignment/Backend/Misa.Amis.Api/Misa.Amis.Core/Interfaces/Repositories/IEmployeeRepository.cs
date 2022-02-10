using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  public interface IEmployeeRepository : IBaseRepository<Employee>, IPageableRepository<Employee>
  {
    /// <summary>
    /// Lay ra EmployeeCode moi nhat
    /// </summary>
    /// <returns></returns>
    string GetNextEmployeeCode();
    int BulkDelete(IEnumerable<Guid> ids);
  }
}
