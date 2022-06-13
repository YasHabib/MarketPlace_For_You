using MarketPlaceForYou.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories
{
    public class MKPFYDbContext:DbContext
    {
        public MKPFYDbContext(DbContextOptions<MKPFYDbContext> options) : base(options)
        {
        }

        public DbSet<UsersEntity> Users => Set<UsersEntity>();
        public DbSet<ListingsEntity> Listings => Set<ListingsEntity>();
    }
}
