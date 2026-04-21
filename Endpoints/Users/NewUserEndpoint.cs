using ApiLogin.Models.General;
using ApiLogin.Models.DB;
using ChkAD;
using ApiLogin.Infraestructure.DB;
using ApiLogin.Interfaces;

public static class AuthEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/login", (Users usersdata, IJWTManager _jwtManager) =>
        {
            if (usersdata == null)
                return Results.BadRequest("Faltan datos");

            string Errormsg = "";

            Procedure procedure = new Procedure
            {
                NBase = "CEPA_CONTENEDORES",
                Procedimiento = "tr_LoginW",
                Parametros = new List<Parametros>
                {
                    new Parametros { nombre = "@usuario", valor = usersdata.Name }
                }
            };

            bool activo = DatosSQL<Activo>.UnProcedimiento(procedure, ref Errormsg).Estado;

            if (!activo)
                return Results.Unauthorized();

            if (!LDAP_Admin.Validar(usersdata.Name, usersdata.Password))
                return Results.Unauthorized();

            procedure.NBase = "CEPA_SADFI";

            string[] argumentos = { usersdata.Name };

            procedure.Procedimiento = PASE.SQLASE(PASE.NSQLASE.Empleado_Usuario, argumentos);
            procedure.Parametros = null;

            Usuario usuario = DatosASE<Usuario>.UnConsulta(procedure, ref Errormsg);

            if (Errormsg != "OK")
                return Results.Problem(
                    title: "Unauthorized",
                    statusCode: 401
                );

            var token = _jwtManager.Authenticate(usersdata);

            if (token == null)
                return Results.Problem(
                    title: "Unauthorized",
                    statusCode: 401
                );

            usuario.Token = token.Token;
            usuario.RefreshToken = token.RefreshToken;

            return Results.Ok(usuario);

        }).AllowAnonymous();
    }
}