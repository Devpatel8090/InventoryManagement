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
            model.Countries = await _unitOfWork.Country.GetAllCountries();
            model.States = await _unitOfWork.State.GetAllStates(); 
            model.Cities = await _unitOfWork.City.GetAllCities();
            return View(model);
        }

        [Route("AccountPayable/Vendor/GetVendorsDT")]
        [HttpPost]
        public async Task<IActionResult> GetVendorsDT()
        {
            DataTableFilter filter = new DataTableFilter(Request);
            var (VendorsDetails, totalCount) = await _unitOfWork.VendorsDetails.GetAllVendorsDetail(filter);
            DataTable<VendorsDetails> VendorsDetailsDt = new DataTable<VendorsDetails>(VendorsDetails.ToList(), totalCount);
            return Json(VendorsDetailsDt);
        }

        public async Task<JsonResult> GetStateDetailsByCountry(long countryId)
        {            
            var states = await _unitOfWork.State.GetStateByCountry(countryId);
            return Json(states);
        }
        public async Task<JsonResult> GetCityDetailsByState(long stateId)
        {            
            var cities = await _unitOfWork.City.GetCityByState(stateId);
            return Json(cities);
        }

        public async Task<bool> AddOrUpdateVendor(string VendorObj)
        {
            var isDone = await _unitOfWork.VendorsDetails.AddOrUpdateVendor(VendorObj);
            if (isDone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<JsonResult> GetVendorDetailById(long id)
        {
            var vendorDetail = await _unitOfWork.VendorsDetails.GetVendorDetailById(id);
            return Json(vendorDetail);
        }

        public async Task<IActionResult> DeleteVendor(long id)
        {
            bool IsDeleted = await _unitOfWork.VendorsDetails.DeleteVendor(id);
            if(IsDeleted)
            {
                TempData["success"] = "Deleted successfully";                
            }
            else
            {
                TempData["error"] = "Record is Not Deleted";
            }
            return RedirectToAction("VendorPage", "Vendors");
        }
    }
}
