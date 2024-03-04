namespace Domain.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string LinkDocument { get; set; }
        public string AvatarPost {  get; set; }
        public DateTime DatePost { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        //FK
        public string FacultyId {  get; set; }
        public Faculty Faculty { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment>? ListComent { get; set; }
    }
}
