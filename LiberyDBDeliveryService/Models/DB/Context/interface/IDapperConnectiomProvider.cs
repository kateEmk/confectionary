using System.Data.Common;

namespace LibraryDatabaseCoffe.Models.DB.Context.@interface
{
    public interface IDapperConnectionProvider
    {
        public Task<DbConnection> GetConnectionAsync(CancellationToken ct = default);
    }
}
