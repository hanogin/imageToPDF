using Service.DTO;

namespace Service.Interface
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        UserRes GetById(int id);
    }
}
