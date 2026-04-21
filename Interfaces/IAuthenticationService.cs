using ApiLogin.Models;

namespace ApiLogin.Interfaces
{
    public interface IAuthenticationService
    {
       abstract bool Login(string userName, string password);
    }
}
