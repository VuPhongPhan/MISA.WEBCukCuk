using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Repository chung
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// CreatedBy: PVPhong (12/01/2021)
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectStrings_MISA_PVPHONG");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }
        public int Add(TEntity entity)
        {
            // Kết nối tới CSDL:

            // Validate dữ liệu
            var parameter = MappingDbType(entity);
            // Khởi tạo các commandText: 
            var result = _dbConnection.Execute($"Proc_Insert{_tableName}", parameter, commandType: CommandType.StoredProcedure);
            return result;
        }

        public int Delete(Guid id)
        {
            // Kết nối tới CSDL:
            // Khởi tạo các commandText:
            var result = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id = '{id}'", commandType: CommandType.Text);
          //  var result = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Code = NV00289", commandType: CommandType.Text);
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            // Kết nối tới CSDL:
            // Khởi tạo các commandText:
            var entities = _dbConnection.Query<TEntity>($"Proc_Select{_tableName}", commandType: CommandType.StoredProcedure);
            // Dữ liệu trả về
            return entities;
        }

        public TEntity GetById(Guid id)
        {
            // Kết nối tới CSDL:
            // Khởi tạo các commandText:
            var entityId = new Dictionary<string, object>
            {
                { $"{_tableName}Id", id.ToString() }
            };
            TEntity result = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}ById", entityId, commandType: CommandType.StoredProcedure).FirstOrDefault();
         //   TEntity result = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}ById", id.ToString(), commandType: CommandType.Text).FirstOrDefault();
            return result;
        }

        public int Update(TEntity entity)
        {
            // Kết nối tới CSDL:
            // Validate dữ liệu
            var parameter = MappingDbType(entity);
            // Khởi tạo các commandText: 
            var result = _dbConnection.Execute($"Proc_Update{_tableName}", parameter,  commandType: CommandType.StoredProcedure);
            return result;
        }

        private DynamicParameters MappingDbType(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var dbValue = ((bool)propertyValue == true ? 1 : 0);
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }

            }
            return parameters;
        }

        public TEntity GetEntityByProperty(TEntity entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = string.Empty;
            if (entity.EntityState == EntityState.AddNew)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            else if (entity.EntityState == EntityState.Update)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            else
                return null;
            var entityReturn = _dbConnection.Query<TEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
    }
}
