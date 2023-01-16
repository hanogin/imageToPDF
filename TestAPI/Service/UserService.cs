using DAL.Context;
using DAL.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Service.DTO;
using Service.Enum;
using Service.Helper;
using Service.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly SportiveContext _context;
        private readonly AppSettingsSecret _configSecret;
        private readonly ILogger _logger;
        private readonly IDapperBaseRepository _con;

        public UserService(
        SportiveContext context,
        IDapperBaseRepository dapper,
        IOptionsMonitor<AppSettingsSecret> configSecret,
        ILogger logger)
        {
            _configSecret = configSecret.CurrentValue;
            _logger = logger;
            _con = dapper;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            int? instructorId = null;
            var user = _context.AppUsers.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            if(user.UserRoleId == (int)UserRole.Instructor)
            {
                instructorId = _context.AppInstructors.Where(x => x.FirstName == user.FirstName && x.LastName == user.LastName).First().InstructorId;
            }
            //var user = _con.QueryFirstOrDefault<UserModel>("SELECT * FROM App_User WHERE UserName = @UserName AND Password = @Password", model);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, instructorId, token);
        }

        public UserRes GetById(int id)
        {
            //return _con.Query<UserRes>("SELECT * FROM App_User WHERE UserId = @UserId", new { UserId = userId }).First();

            return _context.AppUsers.Where(x => x.UserId == id).Select(x => new UserRes
            {
                UserId = id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.UserName,
                UserRoleId = x.UserRoleId
            }).FirstOrDefault();
        }

        //// helper methods
        private string generateJwtToken(AppUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configSecret.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
