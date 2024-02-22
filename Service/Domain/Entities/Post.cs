namespace Domain.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> ListImage {  get; set; }
        public DateTime DatePost { get; set; }
        public bool isApproved { get; set; }
        //FK
        public string FacultyId {  get; set; }
        public Faculty Faculty { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment> ListComent { get; set; }
    }
}
