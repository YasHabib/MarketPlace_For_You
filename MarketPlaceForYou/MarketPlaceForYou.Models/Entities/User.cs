﻿using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    public class User
    {
        //Default empty property 
        public User()
        {
        }

        //For updating/Add user informations
        public User(UserAddVM src)
        {
            Id = src.Id;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Address = src.Address;
            Phone = src.Phone;
            City = src.City;
            Email = src.Email;
        }

        public User(UserUpdateVM src)
        {
            Id = src.Id;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Address = src.Address;
            Phone = src.Phone;
            City = src.City;
        }


        [Key]
        public string? Id { get; set; }
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        [Required]
        public string? Email { get; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? City { get; set; }

        //listing user has created
        public ICollection<Listing> Listings { get; set; }


    }
}
