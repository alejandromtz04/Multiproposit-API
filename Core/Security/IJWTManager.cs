using ApiLogin.Models.Auth;
using ApiLogin.Models.General;

namespace ApiLogin.Core.Security
{
    public interface IJWTManager
    {
        Tokens Authenticate(Users users);
        Tokens RefreshToken(string usuario);
    }
}
