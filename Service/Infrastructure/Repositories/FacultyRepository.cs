using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
            _dbSet.Include(u => u.ListPost).Load();
            _dbSet.Include(u => u.ListAdmin).Load();
        }

        public override void Remove(Faculty faculty)
        {
            faculty.IsDeleted = true;
        }
    }
}
