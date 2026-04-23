using Dapper;
using System.Data;
using ApiLogin.Infraestructure.Data.Connections;
using ApiLogin.Models.General;

namespace ApiLogin.Infraestructure.Data.Repositories.Bus
{
    public class ParadaAutorizadaRepository : IParadaAutorizadaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ParadaAutorizadaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ParadasAutorizadas>> ObtenerParadasAsync(int? idParada)
        {
            // 1 Obtenemos la conexion para BUSES
            using var db = _connectionFactory.CreateSQLConnection("UPDP_BUSES");

            // 2 Definimos los parametros
            var parameters = new { id_parada_autorizada = idParada };

            // 3 Dapper ejecuta y mapea automáticamente.
            // NOTA: Los modelos tienen que ser exactamente iguales a los nombres de las columnas que devuelve el SP, si no es asi, entonces
            // no se puede usar un mapeo personalizado en Dapper ya que tienen que coincidir o usar alias.
            // se puede manejar en el SP (SELECT direccion AS direccion_parada) 
            // pero si los nombres coinciden entonces no es necesario hacer nada adicional, ya que Dapper lo hace automáticamente.
            return await db.QueryAsync<ParadasAutorizadas>(
                "SP_CB_OBTENER_PARADAS_AUTORIZADAS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}