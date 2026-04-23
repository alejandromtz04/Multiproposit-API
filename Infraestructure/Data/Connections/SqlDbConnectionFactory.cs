using AdoNetCore.AseClient;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApiLogin.Infraestructure.Data.Connections
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateSQLConnection(string dbName)
        {
            // Busca en tu appsettings.json dentro del nodo "Databases:NombreBD"
            var dbSection = _config.GetSection($"Databases:{dbName}");

            if (!dbSection.Exists())
                throw new Exception($"La base de datos '{dbName}' no está configurada en appsettings.");

            var server = dbSection["Server"];
            var user = dbSection["User"];
            var password = dbSection["Password"];
            var trust = dbSection["TrustServerCertificate"];

            // Armamos el string de conexión dinámicamente
            var connStr = $"Server={server};Database={dbName};User Id={user};Password={password};TrustServerCertificate={trust};";

            var connection = new SqlConnection(connStr);

            // Nota: Con Dapper ya no necesitas hacer connection.Open() manualmente, 
            // Dapper lo abre y lo cierra automáticamente para ahorrar recursos.
            return connection;
        }

        public IDbConnection CreateASEConnection()
        {
            var aseSection = _config.GetSection("ASE");

            if (!aseSection.Exists())
                throw new InvalidOperationException("Configuración ASE no encontrada.");

            var connStr = $"Data Source={aseSection["Server"]};Port={aseSection["Port"]};Database={aseSection["Database"]};Uid={aseSection["User"]};Pwd={aseSection["Password"]};";

            return new AseConnection(connStr);
        }
    }
}
