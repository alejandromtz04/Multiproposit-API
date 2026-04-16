using ApiLogin.Models;

namespace ApiLogin.Core.Security
{
    public interface IAuthenticationService
    {
       abstract bool Login(string userName, string password);
    }
}
