using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Helper;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Helper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettingsSecret _configSecret;
        private IUserService _userService;

        public JwtMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            IOptionsMonitor<AppSettingsSecret> configSecret
            )
        {
            _next = next;
            _configSecret = configSecret.CurrentValue;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configSecret.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

                // attach user to context on successful jwt validation
                //context.Items["User"] = _userData.GetUser(userId);
                context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }

}
