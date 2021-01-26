using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// API nhân viên
    /// </summary>
    /// CreatedBy: PVPhong (07/01/2021)
    public class EmployeesController : BaseEntityController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("filter")]
        public IActionResult GetFilter([FromQuery] string keySearch, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId)
        {
            var employees = _employeeService.GetFilterEmployee(keySearch, departmentId, positionId);
            return Ok(employees);
        }
        
        [HttpGet("maxcode")]
        public IActionResult GetMaxCode()
        {
            var employeeCode = _employeeService.GetMaxCode();
            return Ok(employeeCode);
        }
    }
}
