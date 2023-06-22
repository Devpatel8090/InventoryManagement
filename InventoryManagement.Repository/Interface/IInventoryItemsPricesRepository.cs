using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface IInventoryItemsPricesRepository
    {
        Task<IEnumerable<InventoryItemsPrices>> GetAllPrices();
        Task<string> GetInventoryItemPriceById(long priceId);
        Task<bool> AddOrUpdateItemPrice(InventoryViewModel model);
        Task<IEnumerable<InventoryItemsPrices>> DeleteItemPrice(long priceId);
        Task<InventoryViewModel> SearchPrice(string searchString);
         Task<int> GetTotalPrices();

        Task<string> GetPriceByItemId(long itemId);
        Task<InventoryViewModel> ItemsPricesPagination(long pageNo);
    }
}
