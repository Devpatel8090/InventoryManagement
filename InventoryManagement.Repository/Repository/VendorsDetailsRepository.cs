using Dapper;
using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<(IEnumerable<VendorsDetails>, int)> GetAllVendorsDetail(DataTableFilter model)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = await connection.QueryMultipleAsync("sp_INVVendorsDetails_GetAllVendorsDetailsWithFilter", new { model.PageStart, model.PageLength, model.Search, model.SortBy, model.SortOrder }, commandType: CommandType.StoredProcedure);
                    var allVendorDetails = data.Read<VendorsDetails>();
                    var TotalVendors = data.ReadFirstOrDefault().TotalVendors;

                    return (allVendorDetails, TotalVendors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error => ", ex.Message);
                    throw ex;

                }
                finally
                {
                    _dataAccess.CloseConnection(connection);
                }
            }
        }

        public async Task<bool> AddOrUpdateVendor(string VendorObj)
        {
            try
            {
                var parseObj = JObject.Parse(VendorObj);
                var Id = parseObj.Value<long>("vendorId");
                var FirstName = parseObj.Value<string>("firstName");
                var LastName = parseObj.Value<string>("lastName");
                var Email = parseObj.Value<string>("email");
                var PhoneNumber = parseObj.Value<long>("phoneNumber");
                var CountryId = parseObj.Value<long>("countryId");
                var StateId = parseObj.Value<long>("stateId");
                var CityId = parseObj.Value<long>("cityId");

                await _dataAccess.SaveData("sp_INVVendorsDetails_AddOrUpdateVendor", new { Id, FirstName, LastName, Email, PhoneNumber, CountryId, StateId, CityId });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return false;

            }
        }
        public async Task<VendorsDetails> GetVendorDetailById(long id)
        {
            try
            {
                var vendor = await _dataAccess.GetSingleData<VendorsDetails, dynamic>("sp_INVVendorsDetails_GetVendorDetailById", new { id });
                return vendor;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return null;

            }
        }
        public async Task<bool> DeleteVendor(long id)
        {
            try
            {
                 await _dataAccess.SaveData<dynamic>("sp_INVVendorsDetails_DeleteVendorById", new { id });
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return false;

            }

        }


    }
}
