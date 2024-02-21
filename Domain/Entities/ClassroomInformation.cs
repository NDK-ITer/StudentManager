namespace Domain.Entities
{
    public class ClassroomInformation
    {
        public string IdClassroom { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string LinkAvatar { get; set; }
        public List<User> ListUser { get; set; }
        public List<UserClassroom> ListUserClassroom { get; set; }
    }
}
