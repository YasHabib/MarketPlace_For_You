using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.Entities.Interfaces;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories
{
    public class BaseRepository<TEntity, TId, TDbContext> : IBaseRepository<TEntity, TId>
        where TEntity : notnull, BaseEntity<TId>
        where TDbContext: notnull, DbContext
        where TId : notnull
    {
        protected DbSet<TEntity> _entityDbSet;
        public BaseRepository(TDbContext context)
        {
            _entityDbSet = context.Set<TEntity>();
        }
        public void Create(TEntity entity, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFunction = null)
        {
            if(typeof(TEntity).GetInterfaces().Contains(typeof(IDated)))
                (entity as IDated).Created = DateTime.UtcNow;
            _entityDbSet.Add(entity);
        }
        public async Task<TEntity> GetById(TId id, Func<IQueryable<TEntity>,IQueryable<TEntity>>? queryFunction = null)
        {
            if (typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDeleted)) && (id as ISoftDeleted).IsDeleted == true)
            {

            }


            TEntity? result;
            if (queryFunction == null)
                result = await _entityDbSet.FirstOrDefaultAsync(i => i.Id.Equals(id));
            else
                result = await queryFunction(_entityDbSet).FirstOrDefaultAsync(i => i.Id.Equals(id));
            return result;
        }
        public async Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFunction = null)
        {
            List<TEntity> results;
            if (queryFunction == null)
                results = await _entityDbSet.ToListAsync();
            else
                results = await queryFunction(_entityDbSet).ToListAsync();
            return results;
        }
        public void Update(TEntity entity)
        {
            _entityDbSet.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _entityDbSet.Remove(entity);
        }

        //Helper
        protected bool ImplementsInterface<TInterface>()
        {
            return (typeof(TEntity).GetInterfaces().Contains(typeof(TInterface)));
        }
    }
}
