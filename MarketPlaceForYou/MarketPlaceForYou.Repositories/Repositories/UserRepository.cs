using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MKPFYDbContext _context;

        public UserRepository(MKPFYDbContext context)
        {
            _context = context;
        }
        public void Create(User entity)
        {
            _context.Add(entity);
        }
        public async Task<User> GetById(string id)
        {
            var result = await _context.Users.FirstAsync(i => i.Id == id);
            return result;
        }
        public async Task<List<User>> GetAll()
        {
            var results = await _context.Users.ToListAsync();
            return results;
        }
        public void Update(User entity)
        {
            _context.Update(entity);
        }
        public void Delete(User entity)
        {
            _context.Remove(entity);
        }


    }
}
