using Infrastructure.Context;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public interface IUnitOfWorkService
    {
        IUserService UserService { get; }
        IFacultyService FacultyService { get; }
        IPostService PostService { get; }
        ICommentService CommentService { get; }
        IRoleService RoleService { get; }
    }
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public UnitOfWorkService(UserDbContext context, IMemoryCache cache)
        {
            UserService = new UserService(context, cache);
            FacultyService = new FacultyService(context, cache);
            PostService = new PostService(context, cache);
            CommentService = new CommentService(context, cache);
            RoleService = new RoleService(context, cache);
        }
        public IUserService UserService { get; private set; }
        public IFacultyService FacultyService { get; private set; }
        public IPostService PostService { get; private set; }
        public ICommentService CommentService { get; private set; }
        public IRoleService RoleService { get; private set; }
    }
}
