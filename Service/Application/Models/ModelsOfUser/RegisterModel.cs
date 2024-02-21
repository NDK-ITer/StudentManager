using Microsoft.AspNetCore.Http;

namespace Application.Models.ModelsOfUser
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AvatarFile { get; set; }
        public DateTime Birthday { get; set; }
    }
}
