﻿using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagement.Repository.Interface
{
    public interface IVendorsDetailsRepository
    {
        Task<(IEnumerable<VendorsDetails>, int)> GetAllVendorsDetail(DataTableFilter model);
        Task<bool> AddOrUpdateVendor(string VendorObj);
        Task<VendorsDetails> GetVendorDetailById(long id);
        Task<bool> DeleteVendor(long id);
        Task<IEnumerable<VendorsDetails>> GetAllVendorsWithoutFilter();
        Task<(IEnumerable<purchaseOrderDetails>, int)> GetVendorOrders(long id);
    }
}
