using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class ClassroomRepository : GenericRepository<ClassroomInformation>, IClassroomRepository
    {
        public ClassroomRepository(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
            _dbSet.Include(u => u.ListUser).Load();
            _dbSet.Include(u => u.ListUserClassroom).Load();
        }
    }
}
