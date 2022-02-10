using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  public interface IPageableRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// Phan trang va tim kiem
    /// </summary>
    /// <param name="pageNumber">So ban ghi/trang</param>
    /// <param name="pageSize">So trang</param>
    /// <param name="search">Tu khoa</param>
    /// <returns></returns>
    Page<T> GetPaged(int pageNumber, int pageSize, string? search = null);
  }
}
