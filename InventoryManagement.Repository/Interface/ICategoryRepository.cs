
using InventoryManagement.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
        public int AddOrUpdateCategories(InventoryViewModel model);

        public string EditCategory(long categoryId);
        public int DeleteCategory(long categoryId);
        Task<InventoryViewModel> SearchCategory(string searchString);

        //Task<IEnumerable<Category>> CategoryPagination(long pageNo);

        Task<InventoryViewModel> CategoryPagination(long pageNo);

        Task<InventoryViewModel> SearchCategoryWithPagination(string searchString, int pageNo = 1, int pageSize = 3);


    }
}
