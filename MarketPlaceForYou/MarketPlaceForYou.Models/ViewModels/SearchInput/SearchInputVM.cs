using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.SearchInput
{/// <summary>
/// Search input to give back to end user
/// </summary>
    public class SearchInputVM
    {/// <summary>
    /// COnstructor for searchinput
    /// </summary>
    /// <param name="src"></param>
        public SearchInputVM(Entities.SearchInput src)
        {
            SearchString = src.SearchString;
        }

        /// <summary>
        /// Search String to be displayed in history if needed
        /// </summary>
        public string SearchString { get; set; } = string.Empty;
    }
}
