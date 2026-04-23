using Dapper;
using ApiLogin.Infraestructure.Data.Connections;
using System.Data;

namespace ApiLogin.Infraestructure.Data.Repositories
{
    // estos serian remplazos para las conexiones futuras
    public abstract class SqlRepositoryBase
    {
        private readonly IDbConnectionFactory _connectionFactory;

        protected SqlRepositoryBase(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected IDbConnection CreateConnection(string dbName)
        {
            return _connectionFactory.CreateSQLConnection(dbName);
        }

        protected async Task<int> ExecuteAsync(
            string dbName,
            string sqlOrProcedure,
            object? parameters = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection(dbName);
            return await connection.ExecuteAsync(sqlOrProcedure, parameters, commandType: commandType);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(
            string dbName,
            string sqlOrProcedure,
            object? parameters = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection(dbName);
            return await connection.QueryAsync<T>(sqlOrProcedure, parameters, commandType: commandType);
        }

        protected async Task<T?> QueryFirstOrDefaultAsync<T>(
            string dbName,
            string sqlOrProcedure,
            object? parameters = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection(dbName);
            return await connection.QueryFirstOrDefaultAsync<T>(sqlOrProcedure, parameters, commandType: commandType);
        }
    }
}
