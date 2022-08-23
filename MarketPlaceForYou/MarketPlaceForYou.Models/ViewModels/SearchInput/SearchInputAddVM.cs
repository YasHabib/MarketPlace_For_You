using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.SearchInput
{/// <summary>
/// View model to add the search string into entity
/// </summary>
    public class SearchInputAddVM
    {/// <summary>
    /// Search string which the user will input in the search box
    /// </summary>
        public string SearchString { get; set; } = string.Empty;
    }
}
