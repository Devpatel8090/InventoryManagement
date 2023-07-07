using InventoryManagement.Entities.ViewModels;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public SalesInvoiceController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async  Task<IActionResult> SalesInvoicePage()
        {
            AccountReceivableViewModel model = new AccountReceivableViewModel()
            {
                customers = await _unitOfWork.CustomerDetails.GetAllCustomersWithoutFilter(),
                Items = await _unitOfWork.InventoryItems.GetInventoryItems(),
            };
            return View(model);
           
        }
        [HttpPost]
        public async Task<bool> AddSalesInvoiceData(string salesObj)
        {
            var isDone = await _unitOfWork.SalesInvoice.AddOrUpdateSalesInvoice(salesObj);
            if (isDone)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<IActionResult> CustomerOrders(int id)
        {
            AccountPayableViewModel model = new AccountPayableViewModel();
            var (VendorsDetails, totalCount) = await _unitOfWork.VendorsDetails.GetVendorOrders(id);
            model.purchaseOrderDetails = VendorsDetails;

            return PartialView("VendorOrders", model);



        }
    }
}
