using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public class Employee : BaseEntity
    {
        #region Declare

        #endregion

        #region Constructor
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Primary]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [IsDuplicate]
        [Required]
        [MaxLength(8, "Mã nhân viên không được quá 50 kí tự")]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        [Required]
        [MaxLength(50, "Tên nhân viên không được quá 50 kí tự")]
        [DisplayName("Tên nhân viên")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính (0-Nữ, 1- Nam, 2- Khác...)
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        ///  Số CMND/Thẻ căn cước
        /// </summary>
        [IsDuplicate]
        [Required]
        [MaxLength(12, "CMND - Thẻ căn cước không được quá 12 kí tự")]
        [DisplayName("CMND - Thẻ căn cước")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        [MaxLength(50, "Địa chỉ không được quá 50 kí tự")]
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [IsDuplicate]
        [Required]
        [IsNotEmail]
        [MaxLength(50, "Email không được quá 8 kí tự")]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        ///  Số điện thoại
        /// </summary>
        [IsDuplicate]
        [Required]
        [MaxLength(11, "Số điện thoại không được quá 11 kí tự")]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Nhóm chức vụ
        /// </summary>
        [Primary]
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Nhóm phòng ban
        /// </summary>
        [Primary]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã số thuế của công ty
        /// </summary>
        [MaxLength(50, "Mã số thuế công ty không được quá 50 kí tự")]
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Lương cơ bản
        /// </summary>
        public double? Salary { get; set; }

        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public int WorkStatus { get; set; }


        public string PositionName { get; set; }

        public string DepartmentName { get; set; }

        #endregion

        #region Method

        #endregion
    }
}
