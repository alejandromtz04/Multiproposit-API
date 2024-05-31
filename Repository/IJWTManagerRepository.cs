using ApiLogin.Models;

namespace ApiLogin.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
        Tokens RefreshToken(string usuario);
    }
}
