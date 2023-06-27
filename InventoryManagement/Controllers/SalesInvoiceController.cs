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
        public IActionResult SalesInvoice()
        {
            return View();
        }
    }
}
