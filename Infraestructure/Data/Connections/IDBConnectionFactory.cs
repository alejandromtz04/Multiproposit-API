using System.Data;

namespace ApiLogin.Infraestructure.Data.Connections
{
    public interface IDbConnectionFactory
    {
        // conexion a SQL Server
        IDbConnection CreateSQLConnection(string dataBase);

        // conexion a ASE SADFI
        IDbConnection CreateASEConnection();
    }
}
