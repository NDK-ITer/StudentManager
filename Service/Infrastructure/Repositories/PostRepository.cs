using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private string _keyValueCache;
        public PostRepository(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
            _dbSet.Include(u => u.User).Load();
            _keyValueCache = "postWhichHaveBeenGet";
        }
    }
}
