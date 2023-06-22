using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class VendorsDetailsRepository : IVendorsDetailsRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public VendorsDetailsRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }
        public async Task<(IEnumerable<VendorsDetails>, int)> GetAllVendorsDetail()
        {
            try
            {
                var allVendorsDetails = await _dataAccess.GetData<VendorsDetails, dynamic>("sp_INVVendorsDetails_GetAllVendorsDetails", new { });
                return (allVendorsDetails, 12);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                throw ex;

            }
            finally
            {

            }
            
        }
    }
}
