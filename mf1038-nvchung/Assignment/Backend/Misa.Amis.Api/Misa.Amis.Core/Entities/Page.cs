namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Dinh dang json tra ve khi phan trang
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public class Page<T> where T : BaseEntity
  {
    /// <summary>
    /// Tong so trang
    /// </summary>
    public int TotalPages { get; set; }
    /// <summary>
    /// Tong so ban ghi
    /// </summary>
    public int TotalRecords { get; set; }
    /// <summary>
    /// Cac ban ghi tren trang
    /// </summary>
    public IEnumerable<T> Data { get; set; }

    public Page(int totalPages, int totalRecords, IEnumerable<T> data)
    {
      TotalPages = totalPages;
      TotalRecords = totalRecords;
      Data = data;
    }
  }
}
