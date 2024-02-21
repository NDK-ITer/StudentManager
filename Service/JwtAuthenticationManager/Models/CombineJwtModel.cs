namespace JwtAuthenticationManager.Models
{
    public class CombineJwtModel
    {
        public string Id { get; set; }
        public string UserName { get; set;}
        public string Avatar { get; set;}
        public string Email { get; set;}
        public string JwtToken { get; set;}
        public int ExpiresIn { get; set;}
    }
}
