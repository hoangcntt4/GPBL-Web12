using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Services
{
  public interface IEmployeeService : IBaseService<Employee>
  {
    /// <summary>
    /// Lay ra EmployeeCode de xuat cho them moi
    /// </summary>
    /// <returns></returns>
    string NextCode();
  }
}
