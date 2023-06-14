using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace InventoryManagement.Repository.Repository
{
    public class InventoryItemsRepository : IinventoryItemsRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public InventoryItemsRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }


        public async Task<IEnumerable<InventoryItems>> GetInventoryItems()
        {
            try
            {
                var items = await _dataAccess.GetData<InventoryItems, dynamic>("[dbo].sp_INVItems_GetAllInventoryItems", new { });
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }


        }


        public async Task<bool> AddOrUpdateItem(IncomeViewModel model)
        {
            if (model.InventoryItem.ItemId > 0)
            {
                return false;
            }
            else
            {
                try
                {
                    await _dataAccess.SaveData("[dbo].sp_INVItems_AddOrUpdateItems", new { model.InventoryItem.ItemName, model.InventoryItem.Description, model.InventoryItem.CategoryId, model.InventoryItem.IsActive });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }



            }
        }

        public async Task<string> GetInventoryItemById(long itemId)
        {
            try
            {
                var inventoryItem = await _dataAccess.GetData<InventoryItems,dynamic>("[dbo].sp_INVItems_GetItemById", new { itemId });
                var singleData = inventoryItem.FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(singleData);
            }
            catch (Exception ex)
            {
                return null;
            }

        }




    }

}
