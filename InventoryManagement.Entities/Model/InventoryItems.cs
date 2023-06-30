using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class InventoryItems
    {
        public long ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Description { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }  
        public long Quantity { get; set; }
        

        public Boolean IsActive { get; set; }
    }
}
