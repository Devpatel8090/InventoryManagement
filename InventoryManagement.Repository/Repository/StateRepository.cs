using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class StateRepository: IStateRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public StateRepository(IDataAccessRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<State>> GetStateByCountry(long countryId)
        {
            try
            {
                var states = await _dataAccess.GetData<State, dynamic>("sp_INVState_GetAllStateByCountryId", new {countryId });
                return states;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
    }
}
