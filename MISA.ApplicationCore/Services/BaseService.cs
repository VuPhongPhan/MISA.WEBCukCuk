using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region DECLARE
        IBaseRepository<TEntity> _baseRepository;
        ServiceResult _serviceResult;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepositorys)
        {
            _baseRepository = baseRepositorys;
            _serviceResult = new ServiceResult();
        }
        #endregion
        #region Property

        #endregion
        #region Method
        public virtual ServiceResult Add(TEntity entity)
        {
            // Gắn trạng thái - phân biệt validate thêm
            // Thực hiện validate
            bool isValidate = Validate(entity);
            if (isValidate == true)
            {
                _serviceResult.data = _baseRepository.Add(entity);
                _serviceResult.Msg = "Đã thêm dữ liệu thành công.";
                _serviceResult.MISACode = Enums.MISACode.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        public ServiceResult Delete(Guid id)
        {
            _serviceResult.data = _baseRepository.Delete(id);
            return _serviceResult;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public TEntity GetById(Guid id)
        {
            return _baseRepository.GetById(id);
        }

        public ServiceResult Update(TEntity entity)
        {
            // Gắn trạng thái - phân biệt validate sửa
             entity.EntityState = Enums.EntityState.Update;

             bool isValidate = Validate(entity);

             // Thực hiện validate 
             if(isValidate == true)
             {
                 _serviceResult.data = _baseRepository.Update(entity);
                 _serviceResult.Msg = "Đã sửa dữ liệu thành công.";
                 _serviceResult.MISACode = Enums.MISACode.IsValid;
                 return _serviceResult;
             }
             else
             {
                 return _serviceResult;
             }
        }

        private bool Validate(TEntity entity)
        {
            var mes = new List<string>();
            var isValidate = true;
            var notIsValid = "Dữ liệu không hợp lệ";
            // Lấy dữ liệu cần validate
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var displayName = string.Empty;
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).Name;
                }

                // Kiểm tra xem có property trống hay không
                if (property.IsDefined(typeof(Required), false))
                {
                    // Check bắt buộc nhập:
                    if (propertyValue == null)
                    {
                        isValidate = false;
                        mes.Add($"{displayName} - không được phép để trống.");
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Msg = notIsValid;
                    }
                }


                // Kiểm tra xem có attribute cần validate không
                if (property.IsDefined(typeof(IsDuplicate), false))
                {
                    // check trùng dữ liệu:
                    var propertyName = property.Name;
                    var entityDulicate = _baseRepository.GetEntityByProperty(entity, property);
                    if(entityDulicate != null)
                    {
                        isValidate = false;
                        mes.Add($"{displayName} - bị trùng");
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Msg = notIsValid;
                    }
                }

                // Kiểm tra xem có property bị qua số lượng kí tự hay không
                if (property.IsDefined(typeof(MaxLength), false))
                {

                    // check quá kí tự
                    var attrMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];
                    var length = (attrMaxLength as MaxLength).length;
                    var msg = (attrMaxLength as MaxLength).msg;
                    if (propertyValue.ToString().Trim().Length > length)
                    {
                        isValidate = false;
                        mes.Add(msg);
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Msg = "Dữ liệu không hợp lệ";
                    }
                }
                if (property.IsDefined(typeof(IsNotEmail), false))
                {
                    try
                    {
                        var mail = new MailAddress(propertyValue.ToString());
                        bool isValidEmail = mail.Host.Contains(".");
                        if (!isValidEmail)
                        {
                            isValidate = false;
                            mes.Add("Email không hợp lệ");
                        }
                        
                    }
                    catch (Exception)
                    {
                        isValidate = false;
                        mes.Add("Email không hợp lệ");
                    }
                }
            }
            _serviceResult.data = mes;
            return isValidate;
        }
        #endregion

    }
}
