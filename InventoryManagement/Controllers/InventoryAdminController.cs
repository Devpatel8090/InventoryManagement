using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace RegistrationDemo.Controllers
{
    public class InventoryAdminController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public InventoryAdminController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Income()
        {
            IncomeViewModel model = new IncomeViewModel
            {
                Categories = _unitOfWork.Category.GetCategories(),
                InventoryItems = await _unitOfWork.InventoryItems.GetInventoryItems()
            };

            return View(model);
        }
        public IActionResult Payee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(IncomeViewModel model)
        {

            var rowsAffected = _unitOfWork.Category.AddOrUpdateCategories(model);
            if (rowsAffected > 0)
            {
                TempData["success"] = "Added Completed Successfully";
                return RedirectToAction("Income");
            }
            else
            {
                TempData["error"] = "Oh No! Please Try Again with Different CategoryName";
                return RedirectToAction("Income");

            }




        }

        public string EditCategory(long categoryId)
        {
            if (categoryId == 0)
            {
                TempData["error"] = "sorry!!!";
                return null;
            }
            else
            {
                var category = _unitOfWork.Category.EditCategory(categoryId);
                return category;
            }
        }

        public IActionResult DeleteCategory(long categoryId)
        {
            if (categoryId == 0)
            {
                TempData["error"] = "sorry!!!";
                return RedirectToAction("Income");
            }
            else
            {
                var affectedRows = _unitOfWork.Category.DeleteCategory(categoryId);
                if (affectedRows > 0)
                {
                    TempData["success"] = "Deleted Successfully";
                }
                else
                {
                    TempData["error"] = "sorry!!!";
                }

                return RedirectToAction("Income");
            }
        }

        public async Task<IActionResult>/*<IEnumerable<Category>>*/ SearchCategory(string searchText)
        {
            if (searchText == null)
            {
                TempData["error"] = "Please Type search string in search box";
                return null;

            }
            else
            {
                var searchtext = searchText.ToLower().Trim(' ');



                /*var Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                return PartialView(Categories );*/

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                incomeViewModel.Categories = await _unitOfWork.Category.SearchCategory(searchtext);

                return PartialView("_CategoryModal", incomeViewModel);



            }
        }

        public async Task<IActionResult> AddInventoryItem(IncomeViewModel model)
        {

            var IsDone = await _unitOfWork.InventoryItems.AddOrUpdateItem(model);
            if (IsDone)
            {
                TempData["success"] = "Item Added  Successfully";
                return RedirectToAction("Income");
            }
            else
            {
                TempData["error"] = "Oh No! Please Try Again with Different ItemName";
                return RedirectToAction("Income");

            }


        }

        public async Task<string> EditInventoryItem(long itemId)
        {
            if (itemId == 0)
            {
                TempData["error"] = "sorry!!!";
                return null;
            }
            else
            {
                var items = await _unitOfWork.InventoryItems.GetInventoryItemById(itemId);
                return items;
            }
        }
    }
}
