using Dapper;
using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
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


        #region GetMethods 
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


        public async Task<string> GetInventoryItemById(long itemId)
        {
            try
            {
                var inventoryItem = await _dataAccess.GetData<InventoryItems, dynamic>("[dbo].sp_INVItems_GetItemById", new { itemId });
                var singleData = inventoryItem.FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(singleData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetTotalItems()
        {


            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var count = connection.ExecuteScalar("[dbo].sp_INVItems_GetCountOfItems");
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


        public async Task<IncomeViewModel> SearchItems(string searchString)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = connection.QueryMultiple("[dbo].[sp_INVItems_GetItemsBySearch]", new { searchString }, commandType: CommandType.StoredProcedure);
                    var items = data.Read<InventoryItems>();
                    var totalItems = data.ReadFirstOrDefault().TotalCount;
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

        /* public async Task<IEnumerable<InventoryItems>> ItemsPagination(long pageNo)
         {
             try
             {
                 var items = await _dataAccess.GetData<InventoryItems, dynamic>("[dbo].sp_INVItems_Pagination", new { pageNo });
                 return items;
             }
             catch (Exception e)
             {
                 Console.WriteLine("Error => ", e.Message);
                 return null;

             }
         }*/


        public async Task<IncomeViewModel> ItemsPagination(long pageNo)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = connection.QueryMultiple("[dbo].sp_INVItems_Pagination", new { pageNo }, commandType: CommandType.StoredProcedure);
                    var items = data.Read<InventoryItems>();
                    var totalItems = data.ReadFirstOrDefault().TotalCount;
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

        #endregion



        #region PostMethods

        public async Task<bool> AddOrUpdateItem(IncomeViewModel model)
        {
            if (model.InventoryItem.ItemId > 0)
            {
                try
                {
                    await _dataAccess.SaveData("[dbo].sp_INVItems_AddOrUpdateItems", new { model.InventoryItem.ItemId, model.InventoryItem.ItemName, model.InventoryItem.Description, model.InventoryItem.CategoryId, model.InventoryItem.IsActive });
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
                    await _dataAccess.SaveData("[dbo].sp_INVItems_AddOrUpdateItems", new { model.InventoryItem.ItemName, model.InventoryItem.Description, model.InventoryItem.CategoryId, model.InventoryItem.IsActive });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public async Task<IEnumerable<InventoryItems>> DeleteItem(long itemId)
        {
            try
            {
                var isDeleted = false;
                if (itemId > 0)
                {
                    await _dataAccess.SaveData<dynamic>("[dbo].sp_INVItems_DeleteItemById", new { itemId });
                    isDeleted = true;
                }
                if (isDeleted)
                {
                    var items = await _dataAccess.GetData<InventoryItems, dynamic>("[dbo].sp_INVItems_GetAllInventoryItems", new { });
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
