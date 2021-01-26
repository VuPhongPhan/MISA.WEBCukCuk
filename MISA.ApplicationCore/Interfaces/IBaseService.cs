using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// Interface chung cho các entity
    /// </summary>
    /// <typeparam name="TEntity">entity</typeparam>
    /// CreatedBy: PVPhong (07/01/2021)
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>List entity</returns>
        /// CreatedBy: PVPhong (07/01/2021)
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Lấy Entity theo mã
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>1 entity</returns>
        /// CreatedBy: PVPhong (07/01/2021)
        TEntity GetById(Guid id);

        /// <summary>
        /// Thêm entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1</returns>
        /// CreatedBy: PVPhong (07/01/2021)
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        /// CreatedBy: PVPhong (07/01/2021)
        ServiceResult Update(TEntity entity);

        /// <summary>
        /// Xóa entity
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>1</returns>
        /// CreatedBy: PVPhong (07/01/2021)
        ServiceResult Delete(Guid id);
        
    }
}
