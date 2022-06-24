using MarketPlaceForYou.Models.Entities;
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
        Task<UserVM> Update(UserUpdateVM userUpdate); //Do not use entity as return type
        Task<UserVM> GetById(string userId); //to get the user id to update their information into db
        Task<UserVM> Create(UserAddVM userAdd);
    }
}
