using ApiLogin.Infraestructure.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiLogin.Controllers
{
    public class TestEndpoint : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("test-db")]
        public IActionResult TestDB()
        {
            try
            {
                using var conn = ComunDB.GetSQL("CEPA_CONTENEDORES");

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 GETDATE() AS Fecha";

                var result = cmd.ExecuteScalar();

                return Ok(new
                {
                    estado = "OK",
                    conexion = "Exitosa",
                    resultado = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = "ERROR",
                    mensaje = ex.Message,
                    detalle = ex.InnerException?.Message
                });
            }
        }
    }
}
