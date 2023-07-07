using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.ViewModels
{
    public class AccountPayableViewModel
    {
        public VendorsDetails VendorDetail { get; set; }
        public IEnumerable<VendorsDetails> VendorsDetails { get; set; } = Enumerable.Empty<VendorsDetails>();
        public IEnumerable<Country> Countries { get; set; } = Enumerable.Empty<Country>();
        public IEnumerable<City> Cities { get; set; } = Enumerable.Empty<City>();
        public IEnumerable<State> States { get; set; } = Enumerable.Empty<State>();
        public IEnumerable<InventoryItems> Items { get; set; } = Enumerable.Empty<InventoryItems>();
        public IEnumerable<purchaseOrderDetails> purchaseOrderDetails { get; set; } = Enumerable.Empty<purchaseOrderDetails>();

    }
}
