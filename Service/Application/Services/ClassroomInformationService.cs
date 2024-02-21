using Application.Models.ModelsOfClassroom;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IClassroomInformationService
    {
        Tuple<string, ClassroomInformation?> Add(AddClassroomModel acm);
        Tuple<string, ClassroomInformation?> Update(UpdateClassroomModel ucm);
        Tuple<string, bool> Remove(string idClassroom);
    }
    public class ClassroomInformationService : IClassroomInformationService
    {
        private readonly IUnitOfWork unitOfWork;
        public ClassroomInformationService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, ClassroomInformation?> Add(AddClassroomModel acm)
        {
            try
            {
                if (acm == null) return new Tuple<string, ClassroomInformation?>("parameter is null",null);
                var classroom = new ClassroomInformation()
                {
                    IdClassroom = Guid.NewGuid().ToString(),
                    Name = acm.Name,
                    Description = acm.Description,
                    Avatar = acm.Avatar,
                    LinkAvatar = acm.LinkAvatar
                };
                unitOfWork.classroomRepository.Add(classroom);
                unitOfWork.SaveChange();
                return new Tuple<string, ClassroomInformation?>("successful",classroom);
            }
            catch (Exception e)
            {
                return new Tuple<string, ClassroomInformation?>(e.Message,null);
            }
        }

        public Tuple<string, bool> Remove(string idClassroom)
        {
            try
            {
                if (idClassroom.IsNullOrEmpty()) return new Tuple<string, bool>("parameter is null or empty", false);
                var classroom = unitOfWork.classroomRepository.GetById(idClassroom);
                if (classroom == null) return new Tuple<string, bool>($"not found this classroom with id {idClassroom}", false);
                unitOfWork.classroomRepository.Remove(classroom);
                unitOfWork.SaveChange();
                return new Tuple<string, bool>("successful", true);
            }
            catch (Exception e)
            {
                return new Tuple<string, bool>(e.Message, false);
            }
        }

        public Tuple<string, ClassroomInformation?> Update(UpdateClassroomModel ucm)
        {
            try
            {
                if (ucm == null) return new Tuple<string, ClassroomInformation?>("parameter is null", null);
                var classroom = unitOfWork.classroomRepository.GetById(ucm.IdClassroom);
                if(classroom == null) return new Tuple<string, ClassroomInformation?>($"not found this classroom with id {ucm.IdClassroom}",null);
                if (ucm.Name.IsNullOrEmpty()) { classroom.Name = ucm.Name; }
                if (ucm.Description.IsNullOrEmpty()) { classroom.Description = ucm.Description; }
                if (ucm.LinkAvatar.IsNullOrEmpty()) { classroom.LinkAvatar = ucm.LinkAvatar; }
                if (ucm.Avatar.IsNullOrEmpty()) { classroom.Avatar = ucm.Avatar; }
                unitOfWork.classroomRepository.Update(classroom);
                unitOfWork.SaveChange();
                return new Tuple<string, ClassroomInformation?>("successful", classroom);
            }
            catch (Exception e)
            {
                return new Tuple<string, ClassroomInformation?>(e.Message,null);
            }
        }
    }
}
