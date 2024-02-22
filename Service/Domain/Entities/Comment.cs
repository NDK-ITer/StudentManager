namespace Domain.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string Content { get; set; }
        //FK
        public string UserId { get; set; }
        public User User { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
