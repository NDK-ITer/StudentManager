namespace JwtAuthenticationManager.Models
{
    public class CombineJwtModel
    {
        public string UserName { get; set;}
        public string Avatar { get; set;}
        public string JwtToken { get; set;}
        public int Lifetime { get; set;}
    }
}
