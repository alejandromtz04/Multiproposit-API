using ApiLogin.Models;

namespace ApiLogin.Repository
{
    public interface IAuthenticationService
    {
       abstract bool Login(string userName, string password);
    }
}
