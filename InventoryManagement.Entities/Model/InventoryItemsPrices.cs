using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class InventoryItemsPrices
    {
        public long PriceId { get; set; }

        public long ItemId { get; set; }
        public string ItemName { get; set; }
        //public long CategoryId { get; set; }
        //public string CategoryName { get; set; }    
        public long Price { get; set; }
            
        
    }
}
