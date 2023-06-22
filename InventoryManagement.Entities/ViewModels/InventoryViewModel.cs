using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class InventoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }  = Enumerable.Empty<Category>();

        public InventoryItems InventoryItem { get; set; }

        public int TotalCategories { get; set; }
        public int TotalInventoryItems { get; set; }
        public int TotalInventoryItemsPrices { get; set; }
        public IEnumerable<InventoryItems> InventoryItems { get; set; }

        public IEnumerable<InventoryItemsPrices> InventoryItemsPricies { get; set; }

        public InventoryItemsPrices InventoryItemPrice { get; set; }

        
    }
}
