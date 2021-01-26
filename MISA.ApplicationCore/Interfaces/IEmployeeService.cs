using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// Interface API nhân viên
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Danh sách nhân viên tìm kiếm
        /// </summary>
        /// <param name="keySearch">Tên, Sđt, ...</param>
        /// <param name="positionId">Id chức vụ</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns>List nhân viên</returns>
        /// CreatedBy: PVPhong (22/01/2021)
        List<Employee> GetFilterEmployee(string keySearch, Guid? departmentId, Guid? positionId);
    }
}
