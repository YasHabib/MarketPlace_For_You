using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// Upload enitty
/// </summary>
    public class Upload : BaseEntity<Guid>
    {   
        /// <summary>
        /// S3 url of the uploaded file
        /// </summary>
        [Required]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key
        /// </summary>
        public Guid? ListingId { get; set; }
    }
}
