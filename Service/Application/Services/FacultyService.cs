using Application.Models.ModelsOfFaculty;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IFacultyService
    {
        Tuple<string, Faculty?> Add(string userId, AddFacultyModel a);
        Tuple<string, Faculty?> Update(string userId, EditFacultyModel e);
        Tuple<string, bool> Delete(string userId, string id);
        Tuple<string, Faculty?> GetById(string id);
        Tuple<string, List<Faculty?>?> GetAll(string userId);
        Tuple<string, bool> AlowUploadPost(string userId, string facultyId);
        Tuple<string, List<Faculty?>?> GetPublic();
    }
    public class FacultyService:IFacultyService
    {
        private readonly IUnitOfWork unitOfWork;

        public FacultyService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, Faculty?> Add(string userId, AddFacultyModel a)
        {
            if (a == null) return new Tuple<string, Faculty?>("parameter is null", null);
            if (userId.IsNullOrEmpty()) return new Tuple<string, Faculty?>("userId not null", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, Faculty?>($"not found with userId: {userId}", null);
            if (user.Role.Name != "ADMIN") return new Tuple<string, Faculty?>("you cant do this", null);
            var newFaculty = new Faculty()
            {
                Id = Guid.NewGuid().ToString(),
                Name = a.Name,
            };
            unitOfWork.facultyRepository.Add(newFaculty);
            unitOfWork.SaveChange();
            return new Tuple<string, Faculty?>("add successful!", newFaculty);
        }

        public Tuple<string, bool> AlowUploadPost(string userId, string facultyId)
        {
            if (userId.IsNullOrEmpty() || facultyId.IsNullOrEmpty()) return new Tuple<string, bool>("parameter is null", false);
            var user = unitOfWork.userRepository.GetById(userId);
            var faculty = unitOfWork.facultyRepository.GetById(facultyId);
            if (!faculty.ListAdmin.Contains(user)) return new Tuple<string, bool>("you cant do it", false);
            faculty.IsOpen = true;
            unitOfWork.facultyRepository.Update(faculty);
            unitOfWork.SaveChange();
            return new Tuple<string, bool>("open successful", true);
        }

        public Tuple<string, bool> Delete(string userId, string id)
        {
            if (id.IsNullOrEmpty())
            {
                return new Tuple<string, bool>("parameter is null!", false);
            }
            if (userId.IsNullOrEmpty()) return new Tuple<string, bool>("userId not null", false);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, bool>($"not found with userId: {userId}", false);
            if (user.Role.Name != "ADMIN") return new Tuple<string, bool>("you cant do this", false);
            var faculty = unitOfWork.facultyRepository.GetById(id);
            if (faculty == null) return new Tuple<string, bool>($"not found faculty with id: {id}", false);
            unitOfWork.facultyRepository.Remove(faculty);
            unitOfWork.SaveChange();
            return new Tuple<string, bool>("delete successful", true);
        }

        public Tuple<string, List<Faculty?>?> GetAll(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, List<Faculty?>?>("userId not null", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, List<Faculty?>?>($"not found with userId: {userId}", null);
            if (user.Role.Name != "ADMIN") return new Tuple<string, List<Faculty?>?>("you cant do this", null);
            var allFaculty = unitOfWork.facultyRepository.GetAll();
            if (allFaculty.IsNullOrEmpty())
            {
                return new Tuple<string, List<Faculty?>?>("no faculty", null);
            }
            return new Tuple<string, List<Faculty?>?>("",allFaculty);
        }

        public Tuple<string, Faculty?> GetById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return new Tuple<string, Faculty?>($"parameter is null", null);
            }
            var faculty = unitOfWork.facultyRepository.GetById(id);
            if (faculty == null) return new Tuple<string, Faculty?>($"not found with id: {id}", null);
            return new Tuple<string, Faculty?>("", faculty);
        }

        public Tuple<string, List<Faculty?>?> GetPublic()
        {
            var publicfaculty = unitOfWork.facultyRepository.Find(f => f.IsDeleted == false);
            if (publicfaculty.IsNullOrEmpty())
            {
                return new Tuple<string, List<Faculty?>?>("no faculty public", null);
            }
            return new Tuple<string, List<Faculty?>?>("", publicfaculty);
        }

        public Tuple<string, Faculty?> Update(string userId, EditFacultyModel e)
        {
            if (e == null) return new Tuple<string, Faculty?>("parameter is null", null);
            if (userId.IsNullOrEmpty()) return new Tuple<string, Faculty?>("userId not null", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, Faculty?>($"not found with userId: {userId}", null);
            if (user.Role.Name != "ADMIN") return new Tuple<string, Faculty?>("you cant do this", null);
            var faculty = unitOfWork.facultyRepository.GetById(e.Id);
            if (faculty == null) return new Tuple<string, Faculty?>($"not founf with id: {e.Id}", null);
            faculty.Name = e.Name;
            unitOfWork.facultyRepository.Update(faculty);
            unitOfWork.SaveChange();
            return new Tuple<string, Faculty?>("update successfull", faculty);
        }
    }
}
