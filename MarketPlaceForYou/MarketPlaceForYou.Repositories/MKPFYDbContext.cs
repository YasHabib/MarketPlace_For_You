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

        public DbSet<User> Users => Set<User>();
        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<FAQ> FAQs => Set<FAQ>(); 
        public DbSet<Upload> Uploads => Set<Upload>();
        public DbSet<SearchInput> SearchInputs => Set<SearchInput>();
        public DbSet<Notification> Notification => Set<Notification>();
    }
}
