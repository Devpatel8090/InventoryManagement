using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CustomerController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult CustomerPage()
        {
            return View();
        }

        [Route("AccountReceivable/Customer/GetCustomerDT")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerDT()
        {
            DataTableFilter filter = new DataTableFilter(Request);
            var (CustomerDetails, totalCount) = await _unitOfWork.CustomerDetails.GetAllCustomerDetails(filter);
            DataTable<CustomerDetail> CustomerDetailsDt = new DataTable<CustomerDetail>(CustomerDetails.ToList(), totalCount);
            return Json(CustomerDetailsDt);
        }

        public async Task<bool> AddOrUpdateCustomer(string customerObj)
        {
            var isDone = await _unitOfWork.CustomerDetails.AddOrUpdateCustomer(customerObj);
            if (isDone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<JsonResult> GetCustomerDetailById(long id)
        {
            var customerDetail = await _unitOfWork.CustomerDetails.GetCustomerDetailById(id);
            return Json(customerDetail);

        }

        public async Task<IActionResult> DeleteCustomer(long id)
        {
            bool IsDeleted = await _unitOfWork.CustomerDetails.DeleteCustomer(id);
            if (IsDeleted)
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
