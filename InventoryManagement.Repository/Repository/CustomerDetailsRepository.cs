using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public CustomerDetailsRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }
    }
}
