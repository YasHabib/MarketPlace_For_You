using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{
    public class UserUpdateVM
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? City { get; set; }

    }
}
