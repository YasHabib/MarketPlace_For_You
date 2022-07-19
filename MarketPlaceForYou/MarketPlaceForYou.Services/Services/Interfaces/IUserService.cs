﻿using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVM> Create(UserAddVM userAdd);
        Task<UserVM> GetById(string userId); //to get the user by their Id
        Task<List<APUserListVM>> GetAll(string userId);
        //Task UserSales(string userId);
        //Task UserPurchases(string userId); 
        Task<UserVM> Update(UserUpdateVM userUpdate); //Do not use entity as return type
        Task Delete(string Id);

    }
}
