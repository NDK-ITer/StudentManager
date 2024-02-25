namespace JwtAuthenticationManager.Models
{
    public class CombineJwtModel
    {
        public string UserName { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Email { get; set;}
        public string Avatar { get; set;}
        public string JwtToken { get; set;}
        public string Role { get; set;}
        public int Lifetime { get; set;}
    }
}
