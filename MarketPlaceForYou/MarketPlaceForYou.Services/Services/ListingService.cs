﻿using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Models.ViewModels.Upload;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MarketPlaceForYou.Models.Entities.Listing;

namespace MarketPlaceForYou.Services.Services
{
    public class ListingService : IListingService
    {
        private readonly IUnitOfWork _uow;

        public ListingService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //CRUD
        public async Task<ListingVM> Create(ListingAddVM src, string userId, Categories category, Cities city, Conditions condition)
        {
            var images = await _uow.Uploads.GetAll(uploads => uploads.Where(upload => src.UploadIds.Contains(upload.Id)));
            var newListing = new Listing(src, userId);
            newListing.Uploads = images;
            newListing.Status = "Active";
            newListing.Category = category;
            newListing.City = city;
            newListing.Condition = condition;
            _uow.Listings.Create(newListing);
            await _uow.SaveAsync();

            var model = new ListingVM(newListing);

            return model;
        }
        public async Task<ListingVM> Update(ListingUpdateVM src, Cities city, Categories category, Conditions condition)
        {
            //grab entity from database
            var entity = await _uow.Listings.GetById(src.Id, item => item.Include(item => item.User).Include(item => item.Uploads));

            entity.ProdName = src.ProdName;
            entity.Description = src.Description;
            entity.Category = category; 
            entity.City = city;
            entity.Condition = condition;
            entity.Price = src.Price;
            entity.Address = src.Address;

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }

        //retrieving listings
        public async Task<ListingVM> GetById(Guid id)
        {
            var result = await _uow.Listings.GetById(id, item => item.Include(items => items.User).Include(items => items.Uploads));

            var model = new ListingVM(result);
            return model;
        }
        public async Task<List<ListingVM>> GetAll(string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userId).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> Deals(string userid)
        {
            //Listing searchResults;
            var list = new List<Listing>();

            //Grabbing the last 3 searches
            var search = await _uow.SearchInputs.GetAll(items => items.Where(i => i.UserId == userid).OrderByDescending(i => i.SearchedDate).Take(3));

            //Default view if there are no search result stored/saved
            if (search.Count() == 0)
            {
                list = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userid && items.Status == "Active" && items.IsDeleted == false)
                                                                   .Include(items => items.Uploads).Include(items => items.User)
                                                                   .Take(16));
            }
            //Looping through each search strings
            else if (search.Count() != 0)
            {
                foreach (var searchInput in search)
                {
                    var searchResults = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userid && items.Status == "Active" && items.IsDeleted == false &&
                                                                       (items.ProdName.ToLower().Contains(searchInput.SearchString.ToLower()) || items.Description.ToLower().Contains(searchInput.SearchString.ToLower())))
                                                                       .Include(items => items.Uploads).Include(items => items.User)
                                                                       .Take(16));
                    list.AddRange(searchResults);
                }
            }

            var models = list.Select(listing => new ListingVM(listing)).DistinctBy(i => i.Id).OrderBy(i => i.Price).ToList();
            return models;

        }
        //Search and filter
        public async Task<List<ListingVM>> GetAllByCity(Cities city, string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userId && items.City == city && items.IsDeleted == false).Include(items => items.User).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> GetAllByCategory(Categories category, string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userId && items.Category == category && items.IsDeleted == false).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> Search(string searchString, string userId)
        {
            //Saving the search result
            var save = new SearchInput(searchString, userId);

            if (searchString != null)
            {
                _uow.SearchInputs.Create(save);
                save.SearchedDate = DateTime.UtcNow;
                await _uow.SaveAsync();
            }

            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userId && (items.Description.ToLower().Contains(searchString.ToLower()) || items.ProdName.ToLower().Contains(searchString.ToLower())) && items.IsDeleted == false).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        //Search with filters
        public async Task<List<ListingVM>> SearchWithFilters(string userId, string? searchString = null, Cities? city = null, Categories? category = null, Conditions? condition = null, decimal minPrice = 0, decimal maxPrice = 0)
        {
            //Saving the search result
            var save = new SearchInput(searchString, userId);

            if (searchString != null)
            {
                _uow.SearchInputs.Create(save);
                save.SearchedDate = DateTime.UtcNow;
                await _uow.SaveAsync();
            }

            var results = await _uow.Listings.SearchWithFilters(searchString, city, category, condition, minPrice, maxPrice);
            var models = results.Select(listing => new ListingVM(listing)).OrderBy(i => i.Price).ToList();
            return models;
        }

        //User's listings
        public async Task<List<ListingVM>> MyActiveListings(string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userid && items.Status == "Active" && items.IsDeleted == false).Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MySoldListings(string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userid && items.Status == "Sold").Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MyPurchases(string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.BuyerID == userId).Include(items => items.User).Include(items => items.Uploads).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> PendingListings(string userId)
        {
            var results = await _uow.Listings.GetAll(Items => Items.Where(items => items.UserId == userId && items.Status == "Pending").Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }

        public async Task<ListingVM> RequestPurchase(ListingPurchaseVM src, string buyerId)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User).Include(items => items.Uploads));
            entity.BuyerID = buyerId;
            entity.Status = "Pending";
            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        
        public async Task<ListingVM> ConfirmPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User).Include(items => items.Uploads));
            entity.Purchased = DateTime.UtcNow;
            entity.Status = "Sold";

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        public async Task<ListingVM> CancelPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User).Include(items => items.Uploads));

            entity.BuyerID = null;
            entity.Status = "Active";

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;          
        }
        public async Task Delete(Guid id)
        {
            var entity = await _uow.Listings.GetById(id, items => items.Include(items => items.User).Include(items => items.Uploads));
            _uow.Listings.Delete(entity);
            await _uow.SaveAsync();
        }
        //Admin Panel

    }
}
