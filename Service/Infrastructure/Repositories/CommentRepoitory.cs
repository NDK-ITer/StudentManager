using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class CommentRepoitory : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepoitory(UserDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
