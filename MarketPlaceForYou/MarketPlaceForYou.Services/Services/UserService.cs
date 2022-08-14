using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<UserVM> Create(UserAddVM src)
        {
            var newEntity = new User(src);
            _uow.Users.Create(newEntity);
            await _uow.SaveAsync();

            var apiKey = _configuration.GetValue<string>("SendGridAPIKey"); //gives back a 200 but no welcome email.
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("yasin_habib@outlook.com", "Market For You");
            var subject = "Welcome to Market For You";
            string fullName = src.FirstName + " " + src.LastName;
            var to = new EmailAddress(src.Email, fullName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

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
            var result = await _uow.Users.GetById(userId,i => i.Include(i => i.Listings));
            var model = new APUserDetailsVM(result);
            return model;
        }
        public async Task<List<APUserListVM>> GetAll()
        {
            var results = await _uow.Users.GetAll(i => i.Include(i => i.Listings));
            var models = results.Select(users => new APUserListVM(users)).ToList();
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
