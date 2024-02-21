using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private string _keyValueCache;
        public UserRepository(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
            _dbSet.Include(u => u.Role).Load();
            _keyValueCache = "userWhichHaveBeenGet";
        }
    }
}
