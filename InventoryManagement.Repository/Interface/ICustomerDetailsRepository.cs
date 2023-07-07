using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagement.Repository.Interface
{
    public interface ICustomerDetailsRepository
    {
        Task<(IEnumerable<CustomerDetail>, int)> GetAllCustomerDetails(DataTableFilter model);
        Task<IEnumerable<CustomerDetail>> GetAllCustomersWithoutFilter();
        Task<bool> AddOrUpdateCustomer(string customerObj);
        Task<CustomerDetail> GetCustomerDetailById(long id);
        Task<bool> DeleteCustomer(long id);
        Task<(IEnumerable<SalesOrderDetails>, int)> GetCustomerOrders(long id);
    }
}
