using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class SalesItems
    {
        public long Id { get; set; }

        public string DocumentNumber { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
