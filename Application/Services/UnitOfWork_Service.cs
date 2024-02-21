using Infrastructure.Context;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public interface IUnitOfWork_Service
    {
        IUserService UserService { get; }
        IClassroomInformationService ClassroomInformationService { get; }
    }
    public class UnitOfWork_Service : IUnitOfWork_Service
    {
        public UnitOfWork_Service(UserDbContext context, IMemoryCache cache)
        {
            UserService = new UserService(context, cache);
            ClassroomInformationService = new ClassroomInformationService(context, cache);
        }
        public IUserService UserService { get; private set; }
        public IClassroomInformationService ClassroomInformationService { get; private set; }
    }
}
