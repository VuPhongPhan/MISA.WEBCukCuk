using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>list employee</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Lấy dữ liệu theo mã
        /// </summary>
        /// <param name="id">Mã dữ liệu</param>
        /// <returns>1 entity</returns>
        TEntity GetById(Guid id);

        /// <summary>
        /// Thêmentity
        /// </summary>
        /// <param name="TEntity">Entity</param>
        /// <returns>1</returns>
        int Add(TEntity entity);

        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="TEntity">Entity</param>
        /// <returns>1</returns>
        int Update(TEntity entity);

        /// <summary>
        /// Xóa entity
        /// </summary>
        /// <param name="id">ID Entity</param>
        /// <returns>1</returns>
        int Delete(Guid id);

        /// <summary>
        /// Lấy trường dữ liệu của entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="property">trường dữ liệu</param>
        /// <returns>property</returns>
        TEntity GetEntityByProperty(TEntity entity, PropertyInfo property);
    }
}
