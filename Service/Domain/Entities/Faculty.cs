namespace Domain.Entities
{
    public class Faculty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        //FK
        public List<Post> ListPost { get; set; }
        public List<User> ListAdmin { get; set; }
    }
}
