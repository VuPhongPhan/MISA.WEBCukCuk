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
    /// API khách hàng
    /// </summary>
    /// <typeparam name="TEntity">Customer</typeparam>
    /// CreatedBy: PVPhong (12/01/2021)
    public class CustomersController : BaseEntityController<Customer>
    {
        ICustomerService _customerService;

       public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }
    }
}
