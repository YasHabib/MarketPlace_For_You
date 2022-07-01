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
            Id = userInfo.Id;
            FirstName = userInfo.FirstName;
            LastName = userInfo.LastName;
            Email = userInfo.Email;
            Address = userInfo.Address;
            Phone = userInfo.Phone;
            City = userInfo.City;
        }

        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
    }
}
