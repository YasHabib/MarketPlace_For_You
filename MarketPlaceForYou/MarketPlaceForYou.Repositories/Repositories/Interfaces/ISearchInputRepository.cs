﻿using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface ISearchInputRepository
    {
        void SaveSearch(SearchInput entity);
        Task<List<SearchInput>> GetAll(string userId);
    }
}
