using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.ViewModels
{
    public class AccountReceivableViewModel
    {
        public IEnumerable<CustomerDetail> customers { get; set; } = Enumerable.Empty<CustomerDetail>();
        public IEnumerable<InventoryItems> Items { get; set; } = Enumerable.Empty<InventoryItems>();
        public IEnumerable<SalesOrderDetails> salesOrderDetails { get; set; } = Enumerable.Empty<SalesOrderDetails>();
    }
}
