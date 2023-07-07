using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface ISalesInvoiceRepository
    {
        Task<bool> AddOrUpdateSalesInvoice(string salesObj);
        Task<string> GetDocumentNumber();
    }
}
