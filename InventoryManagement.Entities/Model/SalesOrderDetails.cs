using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class SalesOrderDetails
    {
        public long Id { get; set; }

        public string DocumentNumber { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public double Amount { get; set; }
        public double CustomerId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long Discount { get; set; }
        public double InvoiceTotal { get; set; }
    }
}
