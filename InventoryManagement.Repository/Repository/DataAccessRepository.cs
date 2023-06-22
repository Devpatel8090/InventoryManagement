using Dapper;
using InventoryManagement.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class DataAccessRepository : IDataAccessRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
        public DataAccessRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DBconnect").Value;

            _connection = new SqlConnection(_connectionString);
        }

        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
        public void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
      
        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters)
        {
            
            return await _connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<P>(string spName, P parameters)
        {
           
            await _connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
        //public async Task<T> GetSingleValue<T, P>(string spName, P parameters)
        //{
        //    return (T)await _connection.ExecuteScalarAsync(spName, parameters, commandType: CommandType.StoredProcedure);

        //}


        /*  public  IEnumerable<T> MutlipleQuery<P>(string spName, P parameters)
          {
               _connection.QueryMultiple("[dbo].sp_INVCategory_GetCategoriesBySearch", parameters , commandType: CommandType.StoredProcedure)
          }*/
        /*public async Task<T> GetSingleData<T, P>(string spName, P parameters)
        {

            await _connection.QuerySingleOrDefault<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }*/




    }
}
