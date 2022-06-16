using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
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

        public UserService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<UserVM> Create(UserAddVM src)
        {
            var newEntity = new User(src);

            _uow.Users.Create(newEntity);
            await _uow.SaveAsync();

            var model = new UserVM(newEntity);

            return model;
        }

        public async Task<UserVM> GetById(Guid id)
        {
            var result = await _uow.Users.GetById(id);
            var model = new UserVM(result);
            return model;
        }

        //Need clarification on this step to see if this is right
        public async Task<UserVM> Update(UserUpdateVM src)
        {
            //read
            var entity = await _uow.Users.GetById(src.Id);
            //perform
            entity.FirstName = src.FirstName;
            entity.LastName = src.LastName;
            entity.Address = src.Address;
            entity.Phone = src.Phone;
            entity.City = src.City;
            //write
            _uow.Users.Update(entity);
            await _uow.SaveAsync();
            //return the user to front end
            var model = new UserVM(entity);
            return model;
        }
    }
}
