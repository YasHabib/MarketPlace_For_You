﻿using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IEmailService
    {
        Task WelcomeEmail(string email, string firstName, string lastName);
        Task PendingListing(ListingPurchaseVM src, string userId);
    }
}
