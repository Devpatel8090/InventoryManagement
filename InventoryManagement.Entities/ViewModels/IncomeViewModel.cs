using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class IncomeViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }  = Enumerable.Empty<Category>();

        public InventoryItems InventoryItem { get; set; }

        public IEnumerable<InventoryItems> InventoryItems { get; set; }
    }
}
