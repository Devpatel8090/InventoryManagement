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
    }
}
