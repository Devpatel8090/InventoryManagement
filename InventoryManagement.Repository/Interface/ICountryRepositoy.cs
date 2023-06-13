using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface ICountryRepositoy
    {
        public IEnumerable<Country> GetAllCountries();
    }
}
