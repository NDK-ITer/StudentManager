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
        Tuple<string, List<Role?>?> GetAll();
        Tuple<string, Role?> EditNormalizeName(string roleId, string normalizeName);
    }
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoleService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, Role?> EditNormalizeName(string roleId, string normalizeName)
        {
            if (roleId.IsNullOrEmpty()) return new Tuple<string, Role?>("parameter is null", null); 
            var role = unitOfWork.roleRepository.GetById(roleId);
            if (role == null) return new Tuple<string, Role?>($"not found role with id: {roleId}", null);
            role.NormalizeName = normalizeName;
            unitOfWork.roleRepository.Update(role);
            unitOfWork.SaveChange();
            return new Tuple<string, Role?>("Update successful!",role);
        }

        public Tuple<string, List<Role?>?> GetAll()
        {
            var allRole = unitOfWork.roleRepository.GetAll();
            if (allRole == null) return new Tuple<string, List<Role?>?>("list role is empty!", null);
            return new Tuple<string, List<Role?>?>("", allRole);
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
