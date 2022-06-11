using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<TypeEntity>
    {
        /// <summary>
        /// Lấy danh sách Entities
        /// </summary>
        /// <returns></returns>
        /// Created By LQTrung(21/01/2022)
        IEnumerable<TypeEntity> GetAll();

        /// <summary>
        /// Lấy dữ liệu Entity theo entityId
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created By LQTrung(22/01/2022)
        TypeEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created By LQTrung(25/01/2022)
        int Insert(TypeEntity entity);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created By LQTrung(25/01/2022)
        int Update(TypeEntity entity, Guid entityId);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created By LQTrung(22/01/2022)
        int Delete(Guid entityId);

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        /// Created By LQTrung(22/01/2022)
        bool CheckDuplicate(string propName, string propValue, Guid? entityId);
    }
}
