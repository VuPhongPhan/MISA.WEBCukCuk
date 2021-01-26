using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Property
        #endregion

        #region Method
        public List<Employee> GetFilterEmployee(string keySearch, Guid? departmentId, Guid? positionId)
        {
            return _employeeRepository.GetFilterEmployee(keySearch, departmentId, positionId);
        }
        #endregion

    }
}
