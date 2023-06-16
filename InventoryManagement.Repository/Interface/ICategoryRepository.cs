
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
        public int AddOrUpdateCategories(IncomeViewModel model);

        public string EditCategory(long categoryId);
        public int DeleteCategory(long categoryId);
         Task<IEnumerable<Category>> SearchCategory(string searchString);

         Task<IEnumerable<Category>> CategoryPagination(long pageNo);


    }
}
