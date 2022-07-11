using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IFAQRepository
    {
        void Create(FAQ entity);
        Task<List<FAQ>> GetAll();
        Task<FAQ> GetById(Guid id);
        void Update(FAQ entity);
        void Delete(FAQ entity);   
    }
}
