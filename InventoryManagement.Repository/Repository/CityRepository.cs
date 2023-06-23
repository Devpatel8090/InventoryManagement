using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagement.Repository.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly IDataAccessRepository _dataAccess;
        public CityRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }
        public async Task<IEnumerable<City>> GetCityByState(long stateId)
        {
            try
            {
                var cities = await _dataAccess.GetData<City, dynamic>("sp_INVCity_GetAllCityByStateId", new { stateId });
                return cities;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
    }
}
