using LibraryDatabaseCoffe.Models.DB.Context.ConnectionProviders;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using Npgsql;

namespace LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys
{
    public abstract class AbstractRepository
    {
        protected readonly IDapperConnectionProvider ConnectiomProvider;

        protected AbstractRepository(IDapperConnectionProvider connectiomProvider)
        {
            ConnectiomProvider = connectiomProvider;
        }
    }
}
