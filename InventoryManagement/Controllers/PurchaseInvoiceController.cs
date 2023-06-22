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
        public IActionResult PurchaseInvoicePage()
        {
            return View();
        }
    }
}
