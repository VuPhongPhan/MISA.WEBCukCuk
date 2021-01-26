using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// API phòng ban
    /// </summary>
    /// CreatedBy: PVPhong (21/01/2021)
    public class DepartmentsController : BaseEntityController<Department>
    {
        IDepartmentService _service;
        
        public DepartmentsController(IDepartmentService service): base(service)
        {
            _service = service;
        }
    }
}
