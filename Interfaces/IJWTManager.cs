using ApiLogin.Infraestructure.Security;
using ApiLogin.Models.General;

namespace ApiLogin.Interfaces
{
    public interface IJWTManager
    {
        Tokens Authenticate(Users users);
        Tokens RefreshToken(string usuario);
    }
}
