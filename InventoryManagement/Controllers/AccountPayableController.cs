using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class AccountPayableController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public AccountPayableController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult AccountPayable()
        {
            return View();
        }
    }
}
