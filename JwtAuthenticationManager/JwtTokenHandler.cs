using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public static class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "NguyenDuyKhg120802WithMySpecialNameIsNDK";
        private const int JWT_TOKEN_VALIDITY = 20;

        public static CombineJwtModel? GenerateJwtToken(UserInfomationModel userAccount)
        {
            //can be use data on database
            var tokenExprityTimeStamp = DateTime.Now.AddHours(JWT_TOKEN_VALIDITY);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userAccount.Fullname),
                new Claim("Role", userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExprityTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new CombineJwtModel
            {
                Id = userAccount.Id,
                UserName = userAccount.Fullname,
                Email = userAccount.Email,
                Avatar = userAccount.Avatar,
                ExpiresIn = (int)tokenExprityTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
