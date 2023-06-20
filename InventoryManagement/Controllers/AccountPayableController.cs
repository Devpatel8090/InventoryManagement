using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class AccountPayableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
