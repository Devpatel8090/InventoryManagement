using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class PurchaseInvoice
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public string DocumentNumber { get; set; }
        public string Reference { get; set; }
        public string Date { get; set; }
        public long SubTotal { get; set; }
        public long Discount { get; set; }
        public double Amount { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
