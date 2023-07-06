using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Repository
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public PurchaseInvoiceRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }
        public async Task<bool> AddOrUpdatePurchaseInvoice(string purchaseObj)
        {
            try
            {
                var parseObj = JObject.Parse(purchaseObj);
                var vendorId = parseObj.Value<long>("vendorId");
                var documentNumber = parseObj.Value<string>("documentNo");
                var date = parseObj.Value<string>("purchaseDate");
                var reference = parseObj.Value<string>("reference");
                var subTotal = parseObj.Value<long>("subTotal");
                var discount = parseObj.Value<long>("discount");
                var amount = parseObj.Value<double>("totalAmount");
                JArray tableData = (JArray)parseObj["tableData"];

                PurchaseInvoice purchaseInvoice = new PurchaseInvoice()
                {
                    VendorId = vendorId,
                    DocumentNumber = documentNumber,
                    Date = date,
                    Reference = reference,
                    SubTotal = subTotal,
                    Discount = discount,
                    Amount = amount,                
                };
                await _dataAccess.SaveData("sp_INVPurchaseInvoice_AddPurchaseInvoice", new { purchaseInvoice.VendorId, purchaseInvoice.DocumentNumber, purchaseInvoice.SubTotal, purchaseInvoice.Discount, purchaseInvoice.Amount });
                foreach (JObject item in tableData)
                {
                    PurchaseItems purchaseItem = new PurchaseItems()
                    {
                        DocumentNumber = documentNumber,
                        ItemId = item.Value<long>("Id"),
                        Quantity = item.Value<long>("Qty"),
                        Price = item.Value<long>("Price"),
                        Amount = item.Value<double>("Amount")
                    };
                    await _dataAccess.SaveData("sp_INVPurchaseItemsDetails_AddPurchaseItems", new { purchaseItem.DocumentNumber,purchaseItem.ItemId, purchaseItem.Quantity, purchaseItem.Price, purchaseItem.Amount });
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error from AddOrUpdatePurchaseInvoice() => " + ex.Message);
                return false;



            }
        }

        public async Task<string> GetDocumentNumber()
        {
            try
            {
                var docNum = await _dataAccess.GetSingleData<string, dynamic>("sp_INVPurchaseInvoice_GetDocumentNumber", new {});
                return docNum;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => " + ex.Message);
                return null;

            }
        }
    }
}
