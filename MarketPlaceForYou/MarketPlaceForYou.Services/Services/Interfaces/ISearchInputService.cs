using MarketPlaceForYou.Models.ViewModels.SearchInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface ISearchInputService
    {
        Task<SearchInputVM> SaveSearch(SearchInputAddVM src, string userId, string? searchString=null);
        Task<List<SearchInputVM>> Get3(string userid);
    }
}
