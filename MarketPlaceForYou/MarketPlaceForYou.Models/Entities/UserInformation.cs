﻿using MarketPlaceForYou.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    public class UserInformation
    {
        //Default empty property 
        public UserInformation()
        {
        }

        //For updating/Add user informations
        public UserInformation(UserUpdateVM user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            Phone = user.Phone;
            City = user.City;
            Email = user.Email;
        }

        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
