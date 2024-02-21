namespace Domain.Entities
{
    public class UserClassroom
    {
        public string IdUser { get; set; }
        public string IdClassroom { get; set; }
        public bool IsAdmin { get; set; }
        public User User { get; set; }
        public ClassroomInformation Classroom { get; set; }
    }
}
