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
    public class Upload
    {   

        /// <summary>
        /// Id of the uploaded file
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// S3 url of the uploaded file
        /// </summary>
        [Required]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key
        /// </summary>
        public Guid? ListingId { get; set; }
        /// <summary>
        /// Setting relationship with listing table
        /// </summary>
        public Listing? Listing { get; set; }

    }
}
