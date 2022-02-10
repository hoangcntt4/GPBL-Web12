using Misa.Amis.Core.Entities;
using OfficeOpenXml;

namespace Misa.Amis.Core.Interfaces.Services
{
  public interface IBaseService<T> where T : BaseEntity
  {
    /// <summary>
    /// Them moi mot ban ghi
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Insert(T entity);
    /// <summary>
    /// Cap nhat mot ban ghi
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Update(Guid id, T entity);
    ExcelPackage ExportExcel(ISet<string>? excludedProps = null);
  }
}
