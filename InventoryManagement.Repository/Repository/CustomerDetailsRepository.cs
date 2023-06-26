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
using System.Web.Mvc;

namespace InventoryManagement.Repository.Repository
{
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public CustomerDetailsRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

        public async Task<(IEnumerable<CustomerDetail>, int)> GetAllCustomerDetails(DataTableFilter model)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = await connection.QueryMultipleAsync("sp_INVCustomerDetails_GetAllCustomersDetailsWithFilter", new { model.PageStart, model.PageLength, model.Search, model.SortBy, model.SortOrder }, commandType: CommandType.StoredProcedure);
                    var allVendorDetails = data.Read<CustomerDetail>();
                    var TotalVendors = data.ReadFirstOrDefault().TotalCustomers;

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
        public async Task<bool> AddOrUpdateCustomer(string customerObj)
        {
            try
            {
                var parseObj = JObject.Parse(customerObj);
                var Id = parseObj.Value<long>("customerId");
                var FirstName = parseObj.Value<string>("firstName");
                var LastName = parseObj.Value<string>("lastName");
                var Email = parseObj.Value<string>("email");
                var PhoneNumber = parseObj.Value<long>("phoneNumber");
                await _dataAccess.SaveData("sp_INVCustomerDetails_AddOrUpdateCustomer", new { Id, FirstName, LastName, Email, PhoneNumber });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return false;

            }
        }
        public async Task<CustomerDetail> GetCustomerDetailById(long id)
        {
            try
            {
                var customer = await _dataAccess.GetSingleData<CustomerDetail, dynamic>("sp_IINVCustomerDetails_GetCustomerDetailById", new { id });
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return null;

            }
        }
        public async Task<bool> DeleteCustomer(long id)
        {
            try
            {
                await _dataAccess.SaveData<dynamic>("sp_IINVCustomerDetails_DeleteCustomerById", new { id });
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
