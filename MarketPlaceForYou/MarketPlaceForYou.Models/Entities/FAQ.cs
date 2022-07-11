using MarketPlaceForYou.Models.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// FAQ entity class
/// </summary>
    public class FAQ
    {/// <summary>
    /// FAQ empty constructor
    /// </summary>
        public FAQ()
        {
            //empty constructor
        }
        /// <summary>
        /// Add FAQ
        /// </summary>
        public FAQ(FAQaddVM src)
        {
            Title = src.Title;
            Description = src.Description;
        }
        /// <summary>
        /// ID of the FAQ
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the FAQ
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Description of the FAQ
        /// </summary>
        public string? Description { get; set; }
    }
}
