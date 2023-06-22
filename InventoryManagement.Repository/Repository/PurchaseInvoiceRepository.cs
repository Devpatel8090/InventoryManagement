using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public PurchaseInvoiceRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

    }
}
