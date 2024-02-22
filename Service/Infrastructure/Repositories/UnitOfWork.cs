using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public interface IUnitOfWork 
    {
        IUserRepository userRepository { get; }
        IRoleRepository roleRepository { get; }
        IPostRepository postRepository { get; }
        void SaveChange();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;

        public UnitOfWork(UserDbContext context, IMemoryCache cache)
        {
            _context = context;
            userRepository = new UserRepository(context, cache);
            roleRepository = new RoleRepository(context, cache);
            postRepository = new PostRepository(context, cache);
        }

        public IUserRepository userRepository { get; private set; }
        public IRoleRepository roleRepository { get; private set; }
        public IPostRepository postRepository { get; private set; }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
