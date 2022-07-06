﻿using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IListingRepository
    {
        void Update(Listing entity);
        void Create(Listing entity);
        Task<Listing> GetById(Guid id);
        Task<List<Listing>> GetAll(string userId);
        Task<List<Listing>> Search(string searchString, string userid);
        Task<List<Listing>> SearchWithFilters(string searchString, string userid, string? city=null, string? category=null);
        Task<List<Listing>> GetAllByCity(string city, string userid);
        Task<List<Listing>> GetAllByCategory(string category, string userid);
        Task<List<Listing>> Deals(string userid);
        Task<List<Listing>> MyActiveListings(string userId);
        Task<List<Listing>> MySoldListings(string userId);
        Task<List<Listing>> MyPurchases(string userId);
        void Purchase(Listing entity);   
        void Delete(Listing entity);
    }
}
