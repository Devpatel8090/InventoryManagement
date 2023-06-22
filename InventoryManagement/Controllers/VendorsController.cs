using InventoryManagement.Entities.Model;
using InventoryManagement.Entities.ViewModels;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class VendorsController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public VendorsController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> VendorPage()
        {
            AccountPayableViewModel model = new AccountPayableViewModel();
            model.VendorsDetails = await _unitOfWork.VendorsDetails.GetAllVendorsDetail();
            return View(model);
        }

        [Route("AccountPayable/Vendor/GetVendorsDT")]
        [HttpPost]
        public async Task<IActionResult> GetVendorsDT()
        {
            DataTableFilter filter = new DataTableFilter(Request);
            var VendorsDetails = await _unitOfWork.VendorsDetails.GetAllVendorsDetail();
            DataTable<VendorsDetails> VendorsDetailsDt = new DataTable<VendorsDetails>(VendorsDetails.ToList(), VendorsDetails.Count());
            return Json(VendorsDetailsDt);
        }
    }
}
