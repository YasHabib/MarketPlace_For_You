using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.FAQ
{/// <summary>
/// Updating FAQ
/// </summary>
    public class FAQupdateVM
    {/// <summary>
    /// ID which will be called to Update the corrosposnding FAQ
    /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Update the title of the FAQ
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Update the description of FAQ
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
