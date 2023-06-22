using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class SalesInvoiceRepository: ISalesInvoiceRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public SalesInvoiceRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }
    }
}
