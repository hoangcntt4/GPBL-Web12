using MISA.WEB12.Core.Entities;

namespace MISA.WEB12.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Tìm kiếm + phân trang
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created By LQTrung(25/01/2022)
        object GetPaging(string? searchText, int pageIndex, int pageSize);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entitiesId"></param>
        /// <returns></returns>
        /// Created By LQTrung(06/02/2022)
        int DeleteMany(Guid[] employeeIds);

        /// <summary>
        /// Lấy mã nhân viên tự tăng
        /// </summary>
        /// <returns></returns>
        ///  Created By LQTrung(07/02/2022)
        public string GetNextCode();

    }
}
