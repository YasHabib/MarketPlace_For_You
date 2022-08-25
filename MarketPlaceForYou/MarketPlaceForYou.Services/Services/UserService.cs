using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MarketPlaceForYou.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow, IConfiguration configuration)
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

        public async Task<UserVM> GetById(string id)
        {
            var result = await _uow.Users.GetById(id);
            var model = new UserVM(result);
            return model;
        }
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


        //Admin panel
        public async Task<APUserDetailsVM> APGetById(string userId)
        {
            var user = await _uow.Users.GetById(userId,i => i.Include(i => i.Listings));
            var purchases = await _uow.Listings.GetAll(items => items.Where(items => items.BuyerID == userId && items.Status == "Sold"));
            var purchaseCount = purchases.Count();
            var model = new APUserDetailsVM(user, purchaseCount);
            return model;
        }
        public async Task<List<APUserListVM>> GetAll()
        {
            var results = await _uow.Users.GetAll(users => users.Include(users => users.Purchases).Include(users => users.Listings));

            var models = results.Select(users => new APUserListVM
            {
                Id = users.Id,
                FullName = users.FirstName + " " + users.LastName,
                City = users.City,
                Email = users.Email,
                TotalActive = users.Listings != null ? users.Listings.Where(i => i.UserId == users.Id && i.Status == "Active").Count() : 0,
                TotalPurchases = users.Purchases != null ? users.Purchases.Where(i => i.BuyerID == users.Id && i.Status == "Sold").Count() : 0
            }).ToList();
            return models;
        }
        public async Task Delete(string id)
        {
            var entity = await _uow.Users.GetById(id);
            _uow.Users.Delete(entity);
            await _uow.SaveAsync();
        }
        public async Task SoftDelete(string id)
        {
            var entity = await _uow.Users.GetById(id);
            entity.IsDeleted = true;
            _uow.Users.Update(entity);
            await _uow.SaveAsync();
        }
        public async Task BlockUser(string id)
        {
            var entity = await _uow.Users.GetById(id);
            entity.IsBlocked = true;
            _uow.Users.Update(entity);
            await _uow.SaveAsync();
        }
        public async Task UnblockUser(string id)
        {
            var entity = await _uow.Users.GetById(id);
            entity.IsBlocked=false;
            _uow.Users.Update(entity);
            await _uow.SaveAsync();
        }

    }
}
