using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IUploadRepository
    {
        void Create(Upload entity);
        Task<Upload> GetById(Guid Id);
        Task<List<Upload>> GetAll(Func<IQueryable<Upload>,IQueryable<Upload>>? queryFunction);
        Task<List<Upload>> GetAllPerListing(Guid listingId);
        void Update(Upload entity);
        void Delete(Upload entity);
    }
}
