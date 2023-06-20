using Dapper;
using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class InventoryItemsPricesRepository : IInventoryItemsPricesRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public InventoryItemsPricesRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }


        #region GetMethods

        public async Task<IEnumerable<InventoryItemsPrices>> GetAllPrices()
        {
            var allItemsPrices = await _dataAccess.GetData<InventoryItemsPrices, dynamic>("[dbo].sp_INVItemPrices_GetAllInventoryItemsPricies", new { });
            return allItemsPrices;
        }

        public async Task<int> GetTotalPrices()
        {
            
            
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var count = connection.ExecuteScalar("[dbo].sp_INVItemPrices_GetCountOfItemsPricies");
                    return (int)count;
                    //var count2 = _dataAccess.GetSingleValue<int>("[dbo].sp_INVItemPrices_GetCountOfItemsPricies");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error => ", e.Message);
                    return 0;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<string> GetInventoryItemPriceById(long priceId)
        {
            try
            {
                var inventoryItem = await _dataAccess.GetData<InventoryItemsPrices, dynamic>("[dbo].sp_INVItemPrices_GetInventoryItemsPricieById", new { priceId });
                var singleData = inventoryItem.FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(singleData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetPriceByItemId(long itemId)
        {
            try
            {
                var items = await _dataAccess.GetData<InventoryItemsPrices, dynamic>("[dbo].sp_INVItemPrices_GetItemPriceByItemId", new { itemId });
                return System.Text.Json.JsonSerializer.Serialize(items);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error => ", e.Message);
                return null;
            }
        }


        public async Task<IncomeViewModel> SearchPrice(string searchString)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = connection.QueryMultiple("[dbo].sp_INVItems_GetItemsBySearch", new { searchString }, commandType: CommandType.StoredProcedure);
                    var items = data.Read<InventoryItems>();
                    var totalItems = data.ReadFirstOrDefault().totalCount;
                    //var categories = await _dataAccess.GetData<Category, dynamic>("[dbo].sp_INVCategory_GetCategoriesBySearch", new { searchString });
                    IncomeViewModel incomeViewModel = new IncomeViewModel()
                    {
                        InventoryItems = items,
                        TotalInventoryItems = totalItems


                    };
                    return incomeViewModel;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error => ", e.Message);
                    return null;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

        }

        public async Task<IEnumerable<InventoryItemsPrices>> ItemsPricesPagination(long pageNo)
        {
            try
            {
                var itemsPrices = await _dataAccess.GetData<InventoryItemsPrices, dynamic>("[dbo].sp_INVItemPrices_Pagination", new { pageNo });
                return itemsPrices;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error => ", e.Message);
                return null;
            }
        }
        #endregion


        #region PostMethods

        public async Task<bool> AddOrUpdateItemPrice(IncomeViewModel model)
        {
            if (model.InventoryItemPrice.PriceId > 0)
            {
                try
                {
                    await _dataAccess.SaveData("[dbo].sp_INVItemsPrices_AddOrUpdateItemsPrice", new { model.InventoryItemPrice.PriceId, model.InventoryItemPrice.ItemId, model.InventoryItemPrice.Price });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    await _dataAccess.SaveData("[dbo].sp_INVItemsPrices_AddOrUpdateItemsPrice", new { model.InventoryItemPrice.Price });
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error => ", ex.Message);
                    return false;
                }
            }
        }

        public async Task<IEnumerable<InventoryItemsPrices>> DeleteItemPrice(long priceId)
        {
            try
            {
                var isDeleted = false;
                if (priceId > 0)
                {
                    await _dataAccess.SaveData<dynamic>("[dbo].sp_INVItemPrices_DeleteItemPriceById", new { priceId });
                    isDeleted = true;
                }
                if (isDeleted)
                {
                    var items = await _dataAccess.GetData<InventoryItemsPrices, dynamic>("[dbo].sp_INVItemPrices_GetAllInventoryItemsPricies", new { });
                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error => ", e.Message);
                return null;
            }
        }

        #endregion

    }
}
