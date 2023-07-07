using InventoryManagement.Entities.Model;
using InventoryManagement.Entities.ViewModels;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PurchaseInvoiceController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> PurchaseInvoicePage()
        {
            AccountPayableViewModel model = new AccountPayableViewModel()
            {
                VendorsDetails = await _unitOfWork.VendorsDetails.GetAllVendorsWithoutFilter(),
                Items = await _unitOfWork.InventoryItems.GetInventoryItems(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<bool> AddPurchaseInvoiceData(string purchseObj)
        {
            var isDone = await _unitOfWork.PurchaseInvoice.AddOrUpdatePurchaseInvoice(purchseObj);
            if (isDone)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<IActionResult> VendorOrders(int id)
        {
            AccountPayableViewModel model = new AccountPayableViewModel();
            var (VendorsDetails, totalCount) = await _unitOfWork.VendorsDetails.GetVendorOrders(id);
            model.purchaseOrderDetails = VendorsDetails;
           
                return PartialView("VendorOrders", model);
            
           
            
        }
    }
}
