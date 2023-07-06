using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class VendorsDetails
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long PhoneNumber { get; set; }
        
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public long StateId { get; set; }
        public string StateName { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        
        public string DocumentNumber { get; set; }
        public  DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
