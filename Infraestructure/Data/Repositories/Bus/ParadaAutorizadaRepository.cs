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
            // 1. Obtenemos la conexión específica para BUSES
            using var db = _connectionFactory.CreateSQLConnection("UPDP_BUSES");

            // 2. Definimos los parámetros
            var parameters = new { id_parada_autorizada = idParada };

            // 3. Dapper ejecuta y mapea automáticamente.
            // NOTA: Si el nombre de la columna en BD es "direccion" y en tu modelo es "direccion_parada",
            // puedes manejarlo en el SP (SELECT direccion AS direccion_parada...) 
            // o Dapper lo hará si los nombres coinciden exactamente.
            return await db.QueryAsync<ParadasAutorizadas>(
                "SP_CB_OBTENER_PARADAS_AUTORIZADAS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}