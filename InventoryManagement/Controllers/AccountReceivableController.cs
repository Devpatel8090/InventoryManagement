using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class AccountReceivableController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public AccountReceivableController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult AccountReceivable()
        {
            return View();
        }
    }
}
