using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TId>
    {
        void Create(TEntity entity, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFunction = null);
        Task<TEntity> GetById(TId id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFunction = null);
        Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFunction = null);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
