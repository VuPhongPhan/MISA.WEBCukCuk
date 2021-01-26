using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Repository khách hàng
    /// </summary>
    /// CreatedBy: PVPhong (12/01/2021)
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Customer> GetCustomersFilter(string specs, Guid? customerGroupId)
        {
            // build tham số đầu vào cho store:
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerCode", specs);
            parameters.Add("@FullName", specs); 
            parameters.Add("@PhoneNumber", specs);
            parameters.Add("@CustomerGroupId", customerGroupId);
            var customers = _dbConnection.Query<Customer>("Proc_SelectCustomerFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
            return customers;
        }
    }
}
