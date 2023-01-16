using DAL.Entities;

namespace Service.DTO
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public int UserRoleId { get; set; }
        public int? InstructorId  { get; set; }


        public AuthenticateResponse(AppUser user, int? InstructorId, string token)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
            UserRoleId = user.UserRoleId;
            this.InstructorId = InstructorId;
        }
    }
}
