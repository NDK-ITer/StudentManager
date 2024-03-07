namespace Server.Requests.Form
{
    public class EditPostForm
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IFormFile? AvatarPost { get; set; }
        public IFormFile? LinkDocument { get; set; }
        public bool IsApproved { get; set; }
    }
}
