using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.FAQ
{/// <summary>
/// Viewmodel for adding FAQ
/// </summary>
    public class FAQaddVM
    {
        /// <summary>
        /// Title textbox will be viewed by the super admin
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Description texbox viewed by the super admin
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
