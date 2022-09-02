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
        Task<UserVM> Create(UserAddVM userAdd);
        Task<UserVM> GetById(string userId); //to get the user by their Id
        Task<UserVM> Update(UserUpdateVM userUpdate); //Do not use entity as return type

        //Admin panel
        Task<APUserDetailsVM> APGetById(string userId);
        Task<List<APUserListVM>> GetAll();
        Task Delete(string Id);
        Task SoftDelete(string id);
        Task BlockUser(string id);
        Task UnblockUser(string id);
        Task<User> GetUser(string id);



    }
}
