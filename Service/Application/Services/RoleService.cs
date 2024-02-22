using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IRoleService
    {
        Tuple<string, Role?> GetById(string id);
        Tuple<string, Role?> GetByName(string name);
    }
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoleService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, Role?> GetById(string id)
        {
            if (id.IsNullOrEmpty()) return new Tuple<string, Role?>("parameter is null", null);
            var role = unitOfWork.roleRepository.GetRoleById(id);
            if (role == null) return new Tuple<string, Role?>($"not found with id: {id}", null);
            return new Tuple<string, Role?>("", role);
        }

        public Tuple<string, Role?> GetByName(string name)
        {
            if (name.IsNullOrEmpty()) return new Tuple<string, Role?>("parameter is null", null);
            var role = unitOfWork.roleRepository.GetRoleByName(name);
            if (role == null) return new Tuple<string, Role?>($"not found with name: {name}", null);
            return new Tuple<string, Role?>("", role);
        }
    }
}
