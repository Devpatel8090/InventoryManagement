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

        public UnitOfWorkRepository(/*IConfiguration configuration*/ IDataAccessRepository dataAccess)
        {
            /*_configuration = configuration;*/
            _dataAccess = dataAccess;
            Category = new CategoryRepository(_dataAccess);
            InventoryItems = new InventoryItemsRepository(_dataAccess);
            InventoryItemsPricies = new InventoryItemsPricesRepository(_dataAccess);
            PurchaseInvoice = new PurchaseInvoiceRepository(_dataAccess);
            VendorsDetails = new VendorsDetailsRepository(_dataAccess);
            CustomerDetails = new CustomerDetailsRepository(_dataAccess);
            SalesInvoice = new SalesInvoiceRepository(_dataAccess);
            Country = new CountryRepository(_dataAccess);
            State = new StateRepository(_dataAccess);
            City = new CityRepository(_dataAccess);
            /*DataAccess = new DataAccessRepository(_connectionString);*/
        }
        /*public SqlConnection CreateConnection() => new SqlConnection(_connectionString);*/
        public IDataAccessRepository DataAccess { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IinventoryItemsRepository InventoryItems { get; private set; }
        public IInventoryItemsPricesRepository InventoryItemsPricies { get; private set; }
        public IVendorsDetailsRepository VendorsDetails { get; private set; }
        public IPurchaseInvoiceRepository PurchaseInvoice { get; private set; }
        public ICustomerDetailsRepository CustomerDetails { get; private set; }
        public ISalesInvoiceRepository SalesInvoice { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IStateRepository State { get; private set; }
        public ICityRepository City { get; private set; }

    }
}
