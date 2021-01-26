using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Enums
{
    /// <summary>
    /// Xác địng trạng thái Validate
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        NotValid = 400,

        /// <summary>
        /// Thành công
        /// </summary>
        Seccess = 200,

        /// <summary>
        /// Lỗi
        /// </summary>
        Exeption = 500,
    }

    public enum EntityState
    {
        AddNew = 1,
        Update = 2,
        Delete = 3
    }
}
