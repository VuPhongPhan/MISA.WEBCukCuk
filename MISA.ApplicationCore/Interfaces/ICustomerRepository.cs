using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu danh sách khách hàng theo mã, tên, sđt, nhóm khách hàng
        /// </summary>
        /// <param name="specs">mã or tên or sđt</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns>list khách hàng</returns>
        /// CreatedBy: PVPhong (20/01/2021)
        List<Customer> GetCustomersFilter(string specs, Guid? customerGroupId);
    }
}
