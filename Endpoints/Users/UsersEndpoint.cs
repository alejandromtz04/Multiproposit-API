using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using ChkAD;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Cors;
using ApiLogin.Models.DB;
using ApiLogin.Models.General;

using ApiLogin.Infraestructure.DB;
using ApiLogin.Interfaces;

namespace ApiLogin.Endpoints
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("")]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManager _jwtManager;
        
        public UsersController(IJWTManager jwtManager)
        {
            _jwtManager = jwtManager;
        }
        
        [HttpGet]
        public List<string> Get() 
        {
            var users = new List<string>
            {
                "Satinder Singh",
                "Amit Sarna",
                "Davin Jon"
            };
            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate(Users usersdata)
        {

            if(usersdata == null)
                return BadRequest("Faltan datos");
            string Errormsg = "";
            Procedure procedure = new Procedure();
            procedure.NBase = "CEPA_CONTENEDORES";
            procedure.Procedimiento = "tr_LoginW";
            procedure.Parametros = new List<Parametros>
            {
                new Parametros { nombre = "@usuario", valor = usersdata.Name }
            };
            bool activo = DatosSQL<Activo>.UnProcedimiento(procedure, ref Errormsg).Estado;
            if (activo)
            {         
                if(LDAP_Admin.Validar(usersdata.Name, usersdata.Password))
                {
                    procedure.NBase = "CEPA_SADFI";
                    string[] argumentos = new string[1];
                    argumentos[0] = usersdata.Name;
                    procedure.Procedimiento = PASE.SQLASE(PASE.NSQLASE.Empleado_Usuario, argumentos); //procedure.Procedimiento = PASE.SQLASE(PASE.NSQLASE.Empleado_Usuario, argumentos);
                    procedure.Parametros = null;
                    Usuario usuario = DatosASE<Usuario>.UnConsulta(procedure, ref Errormsg);
                    if(Errormsg == "OK")
                    {
                        var token = _jwtManager.Authenticate(usersdata);
                        if (token == null)
                        {
                            return Unauthorized();
                        }
                        else
                        {
                            usuario.Token = token.Token;
                            usuario.RefreshToken = token.RefreshToken;
                            return Ok(usuario);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("refreshToken")]
        public IActionResult RefreshToken(Users username)
        {
            if (username == null)
                return Unauthorized();
            var token = _jwtManager.RefreshToken(username.Name);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
