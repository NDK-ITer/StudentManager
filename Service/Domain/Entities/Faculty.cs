namespace Domain.Entities
{
    public class Faculty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        //FK
        public List<Post> ListPost { get; set; }
        public string AdminId { get; set; }
        public User Admin { get; set; }
    }
}
