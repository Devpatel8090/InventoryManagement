﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Entities.Model;

namespace InventoryManagement.Repository.Interface
{
    public interface IUnitOfWorkRepository
    {
        public ICountryRepositoy Country { get; }
        public IStudentDetailsRepository StudentDetails { get; }

        public IStateRepository State { get; }

        public ICityRepository City { get; }

        public ICategoryRepository Category { get; }
    }
}
