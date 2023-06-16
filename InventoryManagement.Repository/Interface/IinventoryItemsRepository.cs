using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface IinventoryItemsRepository
    {
        Task<IEnumerable<InventoryItems>> GetInventoryItems();
        Task<bool> AddOrUpdateItem(IncomeViewModel model);

        Task<string> GetInventoryItemById(long itemId);

        Task<IEnumerable<InventoryItems>> DeleteItem(long itemId);
    }
}
