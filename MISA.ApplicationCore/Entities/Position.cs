using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Chức vụ
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public class Position : BaseEntity
    {
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        public int MyProperty { get; set; }
    }
}
