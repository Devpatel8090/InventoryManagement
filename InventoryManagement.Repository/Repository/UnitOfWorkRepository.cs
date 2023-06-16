using InventoryManagement.Repository.Interface;
using InventoryManagement.Repository.Repository;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IDataAccessRepository _dataAccess;

        public UnitOfWorkRepository(IConfiguration configuration, IDataAccessRepository dataAccess)
        {
            _configuration = configuration;
            _dataAccess = dataAccess;

            Category = new CategoryRepository(_dataAccess);
            InventoryItems = new InventoryItemsRepository(_dataAccess);
            InventoryItemsPricies = new InventoryItemsPricesRepository(_dataAccess);
            /*DataAccess = new DataAccessRepository(_connectionString);*/
        }

        /*public SqlConnection CreateConnection() => new SqlConnection(_connectionString);*/

        public IinventoryItemsRepository InventoryItems { get; private set; }

        public IDataAccessRepository DataAccess { get; private set; }



        public ICategoryRepository Category { get; private set; }
        public IInventoryItemsPricesRepository InventoryItemsPricies {  get; private set;}



    }
}
