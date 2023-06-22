using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Entities.Model
{
    public class DataTableFilter
    {
        public int PageStart { get; set; } = 1;
        public int PageLength { get; set; } = 10;
        public string? Search { get; set; } = string.Empty;
        public string? SortBy { get; set; } = string.Empty;
        public string? SortOrder { get; set; } = string.Empty;
        public DataTableFilter(HttpRequest RequestData)
        {
            PageStart = Convert.ToInt32(RequestData.Form["start"]);
            PageLength = Convert.ToInt32(RequestData.Form["length"]);
            Search = RequestData.Form["search[value]"];
            SortBy = RequestData.Form["columns[" + RequestData.Form["order[0][column]"] + "][name]"];
            SortOrder = RequestData.Form["order[0][dir]"];
        }
    }
}
