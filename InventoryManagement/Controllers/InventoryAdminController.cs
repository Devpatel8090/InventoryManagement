using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
                InventoryItems = await _unitOfWork.InventoryItems.GetInventoryItems(),
                InventoryItemsPricies = await _unitOfWork.InventoryItemsPricies.GetAllPrices(),
                //TotalCategories = _unitOfWork.Category.GetCategories().Count(),
                //TotalInventoryItems = 
            };

            return View(model);
        }
        public IActionResult Payee()
        {
            return View();
        }


        #region InventoryCategory Methods

        /*[HttpPost]
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
        }*/

        public string AddCategory(string AddObj)
        {
            var parseObj = JObject.Parse(AddObj);
            var categoryId = parseObj.Value<long>("categoryId");
            var categoryName = parseObj.Value<string>("categoryName");
            var description = parseObj.Value<string>("description");

            Category category = new Category()
            {
                CategoryId = categoryId,
                CategoryName = categoryName,
                Description = description,
            };
            IncomeViewModel model = new IncomeViewModel()
            {
                Category = category,
            };
            var rowsAffected = _unitOfWork.Category.AddOrUpdateCategories(model);
            if (rowsAffected > 0)
            {
                /*TempData["success"] = "Added Completed Successfully";*/
                return "Success";
            }
            else
            {
                /*TempData["error"] = "Oh No! Please Try Again with Different CategoryName";*/
                return "Fail";

            }
        }

        public IActionResult GetCategories()
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel
            {
                Categories = _unitOfWork.Category.GetCategories(),
                TotalCategories = _unitOfWork.Category.GetCategories().Count(),
            };
            return PartialView("_CategoryModal", incomeViewModel);
        }
        public async Task<IActionResult> GetItems()
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel
            {
                InventoryItems = await _unitOfWork.InventoryItems.GetInventoryItems(),
                TotalInventoryItems = await _unitOfWork.InventoryItems.GetTotalItems()
            };
            return PartialView("_InventoryItemsModal", incomeViewModel);
        }

        public async Task<IActionResult> GetPrices()
        {
            var count = await _unitOfWork.InventoryItemsPricies.GetAllPrices();
            IncomeViewModel incomeViewModel = new IncomeViewModel
            {
                InventoryItemsPricies = await _unitOfWork.InventoryItemsPricies.GetAllPrices(),
                TotalInventoryItemsPrices = await _unitOfWork.InventoryItemsPricies.GetTotalPrices()
                
            };
            return PartialView("_PricingItemsModal", incomeViewModel);
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

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                incomeViewModel.Categories = _unitOfWork.Category.GetCategories();
                incomeViewModel.TotalCategories = _unitOfWork.Category.GetCategories().Count();
                return PartialView("_CategoryModal", incomeViewModel);

            }
            else
            {
                var searchtext = searchText.ToLower().Trim(' ');
                /*var Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                return PartialView(Categories );*/

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                //incomeViewModel.Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                incomeViewModel = await _unitOfWork.Category.SearchCategory(searchtext);


                return PartialView("_CategoryModal", incomeViewModel);



            }
        }

        public async Task<IActionResult> CategoryPagination(long pageNo)
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel();
            //incomeViewModel.Categories = await _unitOfWork.Category.CategoryPagination(pageNo);
            incomeViewModel = await _unitOfWork.Category.CategoryPagination(pageNo);

            return PartialView("_CategoryModal", incomeViewModel);

        }

        public async Task<IActionResult> SearchCategoryWithPagination(string searchText, int pageSize = 3, int pageNo = 1)
        {


            IncomeViewModel incomeViewModel = new IncomeViewModel();
            //incomeViewModel.Categories = await _unitOfWork.Category.SearchCategoryWithPagination(searchText, pageNo, pageSize);
            incomeViewModel = await _unitOfWork.Category.SearchCategoryWithPagination(searchText, pageSize, pageNo);
            return PartialView("_CategoryModal", incomeViewModel);

        }

        #endregion




        #region InventoryItems Methods
        //public async Task<IActionResult> AddInventoryItem(IncomeViewModel model)
        //{

        //    var IsDone = await _unitOfWork.InventoryItems.AddOrUpdateItem(model);
        //    if (IsDone)
        //    {
        //        TempData["success"] = "process completed  Successfully";
        //        return RedirectToAction("Income");
        //    }
        //    else
        //    {
        //        TempData["error"] = "Oh No! Please Try Again with Different ItemName";
        //        return RedirectToAction("Income");

        //    }


        //}
        public async Task<string> AddInventoryItem(string AddObj)
        {
            var parseObj = JObject.Parse(AddObj);
            var itemId = parseObj.Value<long>("itemId");            
            var itemName = parseObj.Value<string>("itemName");
            var itemDescription = parseObj.Value<string>("itemDescription");
            var ItemCategory = parseObj.Value<long>("ItemCategory");
            var ItemActive = parseObj.Value<bool>("ItemActive");

            InventoryItems inventoryItem = new InventoryItems()
            {
                ItemId = itemId,
                ItemName = itemName,
                Description = itemDescription,
                CategoryId = ItemCategory,
                IsActive = ItemActive
            };
            IncomeViewModel model = new IncomeViewModel()
            {
               InventoryItem = inventoryItem,
            };

            var IsDone = await _unitOfWork.InventoryItems.AddOrUpdateItem(model);
            if (IsDone)
            {
               
                return "Success";
            }
            else
            {
                TempData["error"] = "Oh No! Please Try Again with Different ItemName";
                return "Fail";

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
        public async Task<IActionResult> DeleteInventoryItem(long itemId)
        {
            if (itemId == 0)
            {
                TempData["error"] = "sorry!!!";
                return null;
            }
            else
            {
                var remainingRows = await _unitOfWork.InventoryItems.DeleteItem(itemId);
                if (remainingRows != null)
                {

                    TempData["success"] = "Deleted Successfully";
                    IncomeViewModel incomeViewModel = new IncomeViewModel();
                    incomeViewModel.InventoryItems = remainingRows;
                    return PartialView("_InventoryItemsModal", incomeViewModel);
                }
                else
                {
                    TempData["error"] = "sorry!!!";
                    return null;
                }
            }
        }
        public async Task<IActionResult> SearchItem(string searchText)
        {
            if (searchText == null)
            {

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                incomeViewModel.InventoryItems = await _unitOfWork.InventoryItems.GetInventoryItems();
                incomeViewModel.TotalInventoryItems = incomeViewModel.InventoryItems.Count();
                return PartialView("_InventoryItemsModal", incomeViewModel);

            }
            else
            {
                var searchtext = searchText.ToLower().Trim(' ');
                /*var Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                return PartialView(Categories );*/

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                //incomeViewModel.Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                incomeViewModel = await _unitOfWork.InventoryItems.SearchItems(searchtext);


                return PartialView("_InventoryItemsModal", incomeViewModel);



            }
        }
        public async Task<IActionResult> ItemsPagination(long pageNo)
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel();
            incomeViewModel= await _unitOfWork.InventoryItems.ItemsPagination(pageNo);

            return PartialView("_InventoryItemsModal", incomeViewModel);

        }

        #endregion


        //              InventoryItemsPrices 


        #region InventoryItemsPrices Methods

        //public async Task<IActionResult> AddInventoryItemsPrice(IncomeViewModel model)
        //{
        //    var IsDone = await _unitOfWork.InventoryItemsPricies.AddOrUpdateItemPrice(model);
        //    if (IsDone)
        //    {
        //        TempData["success"] = "process completed  Successfully";
        //        return RedirectToAction("Income");
        //    }
        //    else
        //    {
        //        TempData["error"] = "Oh No! Please Try Again with Different ItemName";
        //        return RedirectToAction("Income");

        //    }

        //}
        public async Task<string> AddItemPrice(string AddObj)
        {
            var parseObj = JObject.Parse(AddObj);
            var priceId = parseObj.Value<long>("PriceId");
            var itemPrice = parseObj.Value<long>("ItemPrice");
            var itemId = parseObj.Value<long>("ItemId");


            InventoryItemsPrices Price = new InventoryItemsPrices()
            {
                PriceId = priceId,
                ItemId = itemId,
                Price = itemPrice
            };
            IncomeViewModel model = new IncomeViewModel()
            {
                InventoryItemPrice = Price,
            };
            var IsDone = await _unitOfWork.InventoryItemsPricies.AddOrUpdateItemPrice(model);
            if (IsDone)
            {             
                return "Success";
            }
            else
            {                
                return "Fail";
            }

        }
        public async Task<string> EditPrice(long priceId)
        {
            if (priceId == 0)
            {
                TempData["error"] = "sorry!!!";
                return null;
            }
            else
            {
                var items = await _unitOfWork.InventoryItemsPricies.GetInventoryItemPriceById(priceId);
                return items;
            }

        }

        public async Task<IActionResult> DeleteInventoryItemPrice(long priceId)
        {
            if (priceId == 0)
            {
                TempData["error"] = "sorry!!!";
                return null;
            }
            else
            {
                var remainingRows = await _unitOfWork.InventoryItemsPricies.DeleteItemPrice(priceId);
                if (remainingRows != null)
                {

                    TempData["success"] = "Deleted Successfully";
                    IncomeViewModel incomeViewModel = new IncomeViewModel();
                    incomeViewModel.InventoryItemsPricies = remainingRows;
                    return PartialView("_PricingItemsModal", incomeViewModel);
                }
                else
                {
                    TempData["error"] = "sorry!!!";
                    return null;
                }


            }



        }
        public async Task<string> GetPriceByItemId(long itemId)
        {
            var items = await _unitOfWork.InventoryItemsPricies.GetPriceByItemId(itemId);
            return items;
        }

        public async Task<IActionResult> SearchPrice(string searchText)
        {
            if (searchText == null)
            {

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                incomeViewModel.InventoryItemsPricies = await _unitOfWork.InventoryItemsPricies.GetAllPrices();
                incomeViewModel.TotalInventoryItemsPrices = incomeViewModel.InventoryItemsPricies.Count();
                return PartialView("_PricingItemsModal", incomeViewModel);

            }
            else
            {
                var searchtext = searchText.ToLower().Trim(' ');
                /*var Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                return PartialView(Categories );*/

                IncomeViewModel incomeViewModel = new IncomeViewModel();
                //incomeViewModel.Categories = await _unitOfWork.Category.SearchCategory(searchtext);
                incomeViewModel = await _unitOfWork.InventoryItemsPricies.SearchPrice(searchtext);


                return PartialView("_PricingItemsModal", incomeViewModel);



            }
        }



        public async Task<IActionResult> ItemsPricesPagination(long pageNo)
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel();
            incomeViewModel = await _unitOfWork.InventoryItemsPricies.ItemsPricesPagination(pageNo);

            return PartialView("_PricingItemsModal", incomeViewModel);

        }
        #endregion


    }
}
