using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{
    public class UserVM
    {

        public UserVM(Entities.User userInfo)
        {
            FirstName = userInfo.FirstName;
            LastName = userInfo.LastName;
            Email = userInfo.Email;
            Address = userInfo.Address;
            Phone = userInfo.Phone;
            City = userInfo.City;
        }


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
    }
}
