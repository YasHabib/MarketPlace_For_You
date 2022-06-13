using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService (UnitOfWork uow)
        {
            _uow = uow;
        }
        
        //Need clarification on this step to see if this is right
        public async Task<UsersEntity> Update(UserUpdateVM src)
        {
            UsersEntity user = new UsersEntity();

            if (src == null)
            {
                user = new UsersEntity(src); //adds the user to the system.
                _uow.Users.Update(user);
                await _uow.SaveAsync();
            }
            else //updating the user information in the event the user exists.
            {
                user.FirstName = src.FirstName;
                user.LastName = src.LastName;
                user.Address = src.Address;
                user.City = src.City;
                user.Phone = src.Phone;

                _uow.Users.Update(user);
                await _uow.SaveAsync();
            }
            return user;
        }
    }
}
