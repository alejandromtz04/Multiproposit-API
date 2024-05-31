using ApiLogin.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiLogin.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration) 
        { 
            this.iconfiguration = iconfiguration;
        }

        public Tokens Authenticate(Users users)
        {
            DateTime dateTime = DateTime.UtcNow;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                 new Claim(ClaimTypes.Name, users.Name)
                  }),
                NotBefore= dateTime,
                Expires = dateTime.AddMinutes(3), //481 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var rtokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                             new Claim(ClaimTypes.Name, users.Name)
                  }),
                NotBefore = dateTime.AddMinutes(3), //.AddHours(8),
                Expires = dateTime.AddMinutes(5), //.AddHours(9),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var rtoken = tokenHandler.CreateToken(rtokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = tokenHandler.WriteToken(rtoken) };
        }

        public Tokens RefreshToken(string usuario)
        {
            DateTime dateTime = DateTime.UtcNow;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                 new Claim(ClaimTypes.Name, usuario)
                  }),
                NotBefore = dateTime,
                Expires = dateTime.AddMinutes(240), //481 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var rtokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                             new Claim(ClaimTypes.Name, usuario)
                  }),
                NotBefore = dateTime.AddMinutes(240), //.AddHours(8),
                Expires = dateTime.AddMinutes(270), //.AddHours(9),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var rtoken = tokenHandler.CreateToken(rtokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = tokenHandler.WriteToken(rtoken) };
        }
    }
}
