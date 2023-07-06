using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface IPurchaseInvoiceRepository
    {
        Task<bool> AddOrUpdatePurchaseInvoice(string purchaseObj);
        Task<string> GetDocumentNumber();
    }
}
