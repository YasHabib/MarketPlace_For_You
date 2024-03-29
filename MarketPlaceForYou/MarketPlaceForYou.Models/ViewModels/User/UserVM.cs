﻿using MarketPlaceForYou.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{/// <summary>
/// displaying user to....well user
/// </summary>
    public class UserVM
    {
        /// <summary>
        /// USER!!!
        /// </summary>
        /// <param name="userInfo"></param>
        public UserVM(Entities.User userInfo)
        {
            Id = userInfo.Id;
            FirstName = userInfo.FirstName;
            LastName = userInfo.LastName;
            Email = userInfo.Email;
            Address = userInfo.Address;
            Phone = userInfo.Phone;
            City = userInfo.City;
        }
        /// <summary>
        /// User id (auth)
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// User's firstname
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// user's email
        /// </summary>
        public string Email { get; } = string.Empty;

        /// <summary>
        /// user's address
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// user's phone
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// user's city
        /// </summary>
        public string City { get; set; } = string.Empty;

    }
}
