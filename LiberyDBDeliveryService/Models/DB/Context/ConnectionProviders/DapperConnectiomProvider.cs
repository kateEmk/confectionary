using LibraryDatabaseCoffe.Models.DB.Context.Connection;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.Logging;
using System.Configuration;
using System.Data.Common;

namespace LibraryDatabaseCoffe.Models.DB.Context.ConnectionProviders
{
    public class DapperConnectionProvider : IDapperConnectionProvider
    {
        private readonly ConnectionOptions _connectionString;

        public DapperConnectionProvider(IOptions<ConnectionOptions> connectionString)
        {
            _connectionString = connectionString.Value;
        }
        public async Task<DbConnection> GetConnectionAsync(CancellationToken ct = default)
        {
            Console.WriteLine(_connectionString);
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=shop;Username=postgres;Password=postgres");
            await connection.OpenAsync(ct);

            return connection;
        }
    }
}
