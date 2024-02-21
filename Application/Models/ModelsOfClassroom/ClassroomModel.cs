using Domain.Entities;

namespace Application.Models.ModelsOfClassroom
{
    public class ClassroomModel
    {
        public string Name { get; set; }
        public string Decription { get; set; }
        public string LinkAvatar { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
        public ClassroomModel(ClassroomInformation classroom, string idUser)
        {
            this.Name = classroom.Name;
            this.Decription = classroom.Description;
            this.LinkAvatar = classroom.LinkAvatar;
            this.Avatar = classroom.Avatar;
            this.IsAdmin = classroom.ListUserClassroom.FirstOrDefault(p => p.IdUser == idUser).IsAdmin;
        }
    }
}
