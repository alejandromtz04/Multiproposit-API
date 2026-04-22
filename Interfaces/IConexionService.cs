using System.Data;

namespace ApiLogin.Interfaces
{
    public interface IConexionService
    {
        IDbConnection GetSQLConnection(string dbName);
        IDbConnection GetASEConnection();
    }
}
