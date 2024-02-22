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
        Tuple<string, Faculty?> Add(AddFacultyModel a);
        Tuple<string, Faculty?> Update(EditFacultyModel e);
        Tuple<string, bool> Delete(string id);
        Tuple<string, Faculty?> GetById(string id);
        Tuple<string, List<Faculty?>?> GetAll();
    }
    public class FacultyService:IFacultyService
    {
        private readonly IUnitOfWork unitOfWork;
        public FacultyService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, Faculty?> Add(AddFacultyModel a)
        {
            if (a == null) return new Tuple<string, Faculty?>("parameter is null", null);
            var newFaculty = new Faculty()
            {
                Id = Guid.NewGuid().ToString(),
                Name = a.Name,
            };
            unitOfWork.facultyRepository.Add(newFaculty);
            unitOfWork.SaveChange();
            return new Tuple<string, Faculty?>("add successful!", newFaculty);
        }

        public Tuple<string, bool> Delete(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return new Tuple<string, bool>("parameter is null!", false);
            }
            var faculty = unitOfWork.facultyRepository.GetById(id);
            unitOfWork.facultyRepository.Remove(faculty);
            unitOfWork.SaveChange();
            return new Tuple<string, bool>("delete successful", true);
        }

        public Tuple<string, List<Faculty?>?> GetAll()
        {
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

        public Tuple<string, Faculty?> Update(EditFacultyModel e)
        {
            if (e == null) return new Tuple<string, Faculty?>("parameter is null", null);
            var faculty = unitOfWork.facultyRepository.GetById(e.Id);
            if (faculty == null) return new Tuple<string, Faculty?>($"not founf with id: {e.Id}", null);
            faculty.Name = e.Name;
            unitOfWork.facultyRepository.Update(faculty);
            unitOfWork.SaveChange();
            return new Tuple<string, Faculty?>("update successfull", faculty);
        }
    }
}
