using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.SearchInput;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class SearchInputService : ISearchInputService
    {
        private readonly IUnitOfWork _uow;

        public SearchInputService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<SearchInputVM> SaveSearch(SearchInputAddVM src, string userId, string? searchString = null)
        {
            var save = new SearchInput(src, userId);

            if (searchString != null)
            {
                _uow.SearchInputs.SaveSearch(save);
                await _uow.SaveAsync();
            }

            var model = new SearchInputVM(save);
            return model;
        }

        public async Task<List<SearchInputVM>> GetAll(string userId)
        {
            var results = await _uow.SearchInputs.GetAll(userId);
            var models = results.Select(search => new SearchInputVM(search)).ToList();
            return models;
        }
    }
}
