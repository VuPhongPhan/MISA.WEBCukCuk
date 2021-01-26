using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Repository nhân viên
    /// </summary>
    /// CreatedBy: PVPhong (12/01/2021)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {


        #region DECLARE
        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Property
        #endregion
        #region Method
        public List<Employee> GetFilterEmployee(string keySearch, Guid? departmentId, Guid? positionId)
        {
            // build tham số đầu vào cho store:

            var input = keySearch != null ? keySearch : string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", input, DbType.String);
            parameters.Add("@FullName", input, DbType.String);
            parameters.Add("@PhoneNumber", input, DbType.String);
            parameters.Add("@DepartmentId", departmentId, DbType.String);
            parameters.Add("@PositionId", positionId, DbType.String);
            var employees = _dbConnection.Query<Employee>("Proc_SelectEmployeeFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
            return employees;
        }
        #endregion
    }
}
