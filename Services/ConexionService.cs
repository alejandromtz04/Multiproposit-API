using System.Data.SqlClient;
using AdoNetCore.AseClient;

public class ConexionService
{
    private readonly IConfiguration _config;

    public ConexionService(IConfiguration config)
    {
        _config = config;
    }

    public SqlConnection GetSQL(string nbase)
    {
        var baseConn = _config.GetConnectionString("SQL_CON");

        if (string.IsNullOrEmpty(baseConn))
            throw new Exception("SQL_CON no configurado");

        var connStr = baseConn.Replace("BASE_DATOS", nbase);

        var conn = new SqlConnection(connStr);
        conn.Open();

        return conn;
    }

    public AseConnection GetASE()
    {
        var connStr = _config.GetConnectionString("ASE_CON");

        if (string.IsNullOrEmpty(connStr))
            throw new Exception("ASE_CON no configurado");

        var conn = new AseConnection(connStr);
        conn.Open();

        return conn;
    }
}