using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Upload
{   /// <summary>
    /// A View model for listing images
    /// </summary>
    public class ListingImageVM
    {   /// <summary>
        /// Users viewing liisting image
        /// </summary>
        /// <param name="src"></param>
        public ListingImageVM(Entities.Upload src)
        {
            ImageURL = src.Url;
        }
        /// <summary>
        /// AWS S3 image url
        /// </summary>
        public string ImageURL { get; set; }
    }
}
