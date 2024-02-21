namespace Domain.Entities
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NormalizeName { get; set; }
        public List<User> Users { get; set; }
    }
}
