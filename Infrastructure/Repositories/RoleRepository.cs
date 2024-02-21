using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
            _dbSet.Include(c => c.Users).Load();
        }
        public Role? GetRoleById(string id) => GetById(id);
        public Role? GetRoleByName(string name) => Find(u => u.Name == name.ToUpper()).FirstOrDefault();
    }
}
