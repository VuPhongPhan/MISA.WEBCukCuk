using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    /// API khách hàng
    /// </summary>
    /// <typeparam name="TEntity">Customer</typeparam>
    /// CreatedBy: PVPhong (19/01/2021)
    public class CustomerGroupsController : BaseEntityController<CustomerGroup>
    {
        ICustomerGroupService _service;
        public CustomerGroupsController(ICustomerGroupService service) : base(service)
        {
            _service = service;
        }

    }


}
