using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.User
{
    public class UserAddVM
    {
        [Required]
        public string? Id { get; set; }
       
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? City { get; set; }
    }
}
