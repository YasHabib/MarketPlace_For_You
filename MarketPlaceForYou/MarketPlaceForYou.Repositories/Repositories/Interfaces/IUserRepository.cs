﻿using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Update(User entity);
        void Create(User entity);
        Task<User> GetById(Guid id);

        //void Delete(User entity); //For super admin to delete users
    }
}
