using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Upload
{   /// <summary>
    /// Adding images to listing
    /// </summary>
    public class AddImageToListingVM
    {
        ///// <summary>
        ///// Id of the uploaded file
        ///// </summary>
        //public Guid Id { get; set; }
        /// <summary>
        /// Id of the listing these images belongs to
        /// </summary>
        public Guid ListingId { get; set; }
    }
}
