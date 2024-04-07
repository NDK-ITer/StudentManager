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
        Tuple<string, User?> SetManager(string userId, string idFaculty);
        Tuple<string, User?> SetUser(string userId);
        Tuple<string, User?> SetStudent(string userId);
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

        public Tuple<string, User?> SetManager(string userId, string idFaculty)
        {
            if (string.IsNullOrEmpty(userId)) return new Tuple<string, User?>("paramater is null!", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, User?>($"not found user with id: {userId}", null);
            var faculty = unitOfWork.facultyRepository.GetById(idFaculty);
            if (faculty == null) return new Tuple<string, User?>($"not found faculty with id: {idFaculty}", null);
            var role = unitOfWork.roleRepository.Find(r => r.Name == "MANAGER").FirstOrDefault();
            user.RoleId = role.Id;
            user.FacultyID = faculty.Id;
            unitOfWork.userRepository.Update(user);
            unitOfWork.SaveChange();
            return new Tuple<string, User?>("",user);
        }

        public Tuple<string, User?> SetStudent(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new Tuple<string, User?>("paramater is null!", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, User?>($"not found user with id: {userId}", null);
            var role = unitOfWork.roleRepository.Find(r => r.Name == "STUDENT").FirstOrDefault();
            user.RoleId = role.Id;
            if (!user.FacultyID.IsNullOrEmpty()) user.FacultyID = null;
            unitOfWork.userRepository.Update(user);
            unitOfWork.SaveChange();
            return new Tuple<string, User?>("", user);
        }

        public Tuple<string, User?> SetUser(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new Tuple<string, User?>("paramater is null!", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, User?>($"not found user with id: {userId}", null);
            var role = unitOfWork.roleRepository.Find(r => r.Name == "USER").FirstOrDefault();
            user.RoleId = role.Id;
            if (!user.FacultyID.IsNullOrEmpty()) user.FacultyID = null;
            unitOfWork.userRepository.Update(user);
            unitOfWork.SaveChange();
            return new Tuple<string, User?>("", user);
        }
    }
}
