using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class AccountReceiveableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
