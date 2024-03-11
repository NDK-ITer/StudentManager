using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using XAct;

namespace Server.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration["Jwt:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "Id").Value;
                var Role = jwtToken.Claims.First(x => x.Type == "Role").Value;
                context.Items["UserId"] = userId;
                context.Items["Role"] = Role;
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!token.IsNullOrEmpty())
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }
    }
}
