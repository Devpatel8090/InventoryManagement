using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class CountryRepository: ICountryRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public CountryRepository(IDataAccessRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IEnumerable<Country>> GetAllCountries()        
        {
            try
            {
                var countries = await _dataAccess.GetData<Country, dynamic>("sp_INVCountry_GetAllCountries", new { });
                return countries;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
    }
}
