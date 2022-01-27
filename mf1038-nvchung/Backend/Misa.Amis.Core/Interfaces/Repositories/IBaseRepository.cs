using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  /// <summary>
  /// Interface repository base
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public interface IBaseRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// Lay tat ca ban ghi <typeparamref name="T"/>
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> GetAll();
    /// <summary>
    /// Lay mot ban ghi <typeparamref name="T"/> theo khoa chinh
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T GetById(Guid id);
    /// <summary>
    /// Lay mot ban ghi <typeparamref name="T"/> theo code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    T GetByCode(string code);
    /// <summary>
    /// Them moi mot ban ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>So ban ghi duoc them</returns>
    int Insert(T entity);
    /// <summary>
    /// Cap nhat ban ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns>So ban ghi duoc cap nhat</returns>
    int Update(Guid id, T entity);
    /// <summary>
    /// Xoa mot ban ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns>So ban ghi bii xoa</returns>
    int Delete(Guid id);
  }
}
