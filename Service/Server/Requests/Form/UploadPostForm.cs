namespace Server.Requests.Form
{
    public class UploadPostForm
    {
        public string FacultyId { get; set; }
        public string Title { get; set; }
        public IFormFile AvatarPost { get; set; }
        public IFormFile Document { get; set; }
    }
}
