using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public class Customer : BaseEntity
    {
        #region Declare

        #endregion

        #region Constructor
        public Customer()
        {
        }
        #endregion


        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [IsDuplicate]
        [Required]
        [MaxLength(8, "Mã khách hàng không được quá 8 kí tự")]
        [DisplayName("Mã khách hàng")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        [Required]
        [MaxLength(50, "Tên khách hàng không được quá 50 kí tự")]
        [DisplayName("Tên khách hàng")]
        public string FullName { get; set; }

        /// <summary>
        /// Số thẻ thành viên
        /// </summary>
        [MaxLength(10, "Mã thẻ khách hàng không được quá 10 kí tự")]
        [DisplayName("Mã thẻ khách hàng")]
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        [Primary]
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính (0-Nữ, 1- Nam, 2- Khác...)
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [IsDuplicate]
        [Required]
        [MaxLength(50, "Email không được quá 50 kí tự")]
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
        /// Tên công ty
        /// </summary>
        [MaxLength(50, "Tên công ty không được quá 50 kí tự")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        [MaxLength(10, "Mã số thuế công ty không được quá 10 kí tự")]
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(50, "Địa chỉ không được quá 50 kí tự")]
        public string Address { get; set; }


        public string CustomerGroupName { get; set; }

        #endregion

        #region Method

        #endregion
    }
}
