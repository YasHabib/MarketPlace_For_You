using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UsersEntity> Update(UserUpdateVM userUpdate);

        //Task<UserUpdateVM> GetUser(Guid userId); //to get the user id to update their information into db
    }
}
