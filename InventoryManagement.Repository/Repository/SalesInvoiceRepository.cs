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
    public class SalesInvoiceRepository : ISalesInvoiceRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public SalesInvoiceRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

        public async Task<bool> AddOrUpdateSalesInvoice(string salesObj)
        {
            try
            {
                var parseObj = JObject.Parse(salesObj);
                var customerId = parseObj.Value<long>("customerId");
                var documentNumber = parseObj.Value<string>("documentNo");
                var date = parseObj.Value<string>("purchaseDate");
                var reference = parseObj.Value<string>("reference");
                var subTotal = parseObj.Value<long>("subTotal");
                var discount = parseObj.Value<long>("discount");
                var amount = parseObj.Value<double>("totalAmount");
                JArray tableData = (JArray)parseObj["tableData"];

                SalesInvoice salesInvoice = new SalesInvoice()
                {
                    CustomerId = customerId,
                    DocumentNumber = documentNumber,
                    Date = date,
                    Reference = reference,
                    SubTotal = subTotal,
                    Discount = discount,
                    Amount = amount,
                };

                await _dataAccess.SaveData("sp_INVSalesInvoice_AddSalesInvoice", new { salesInvoice.CustomerId, salesInvoice.DocumentNumber, salesInvoice.SubTotal, salesInvoice.Discount, salesInvoice.Amount });
                foreach (JObject item in tableData)
                {
                    SalesItems salesItems = new SalesItems()
                    {
                        DocumentNumber = documentNumber,
                        ItemId = item.Value<long>("Id"),
                        Quantity = item.Value<long>("Qty"),
                        Price = item.Value<long>("Price"),
                        Amount = item.Value<double>("Amount")
                    };
                    await _dataAccess.SaveData("sp_INVSalesItemsDetails_AddSalesItems", new { salesItems.DocumentNumber, salesItems.ItemId, salesItems.Quantity, salesItems.Price, salesItems.Amount });
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error from AddOrUpdateSalesInvoice() => " + ex.Message);
                return false;



            }
        }


        public async Task<string> GetDocumentNumber()
        {
            try
            {
                var docNum = await _dataAccess.GetSingleData<string, dynamic>("sp_INVSalesInvoice_GetDocumentNumber", new { });
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
