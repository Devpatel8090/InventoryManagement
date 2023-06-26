using InventoryManagement.Entities.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagement.Repository.Interface
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCityByState(long stateId);
        Task<IEnumerable<City>> GetAllCities();
    }
}
