using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    // Dùng để check bắt buộc nhập
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    // Dùng để check trùng dữ liệu
    [AttributeUsage(AttributeTargets.Property)]
    public class IsDuplicate : Attribute
    {

    }
    
    // Dùng để check trùng dữ liệu
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotEmail : Attribute
    {

    }

    // Lấy tên bảng
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name = null)
        {
            this.Name = name;
        }
    }

    // Lấy độ dài Property
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int length { get; set; }
        public string msg { get; set; }
        public MaxLength(int length, string msg)
        {
            this.length = length;
            this.msg = msg;
        }
    }

    // Khóa 
    [AttributeUsage(AttributeTargets.Property)]
    public class Primary : Attribute
    {
    }

    // Khóa chính
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính chung
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// Ngày sửa 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
    }

}
