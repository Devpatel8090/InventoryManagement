using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Entities.Model;

namespace InventoryManagement.Repository.Interface
{
    public interface IUnitOfWorkRepository
    {
      
        public IinventoryItemsRepository InventoryItems { get; }
        public ICategoryRepository Category { get; }
        public IInventoryItemsPricesRepository InventoryItemsPricies { get; }
        public IPurchaseInvoiceRepository PurchaseInvoice { get; }
        public IVendorsDetailsRepository VendorsDetails { get; }
        public ICustomerDetailsRepository CustomerDetails { get; }
        public ISalesInvoiceRepository SalesInvoice { get; }
        public ICountryRepository Country { get; }
        public IStateRepository State { get; }
        public ICityRepository City { get; }


        /*        public IDataAccessRepository DataAccess { get; }*/
    }
}
