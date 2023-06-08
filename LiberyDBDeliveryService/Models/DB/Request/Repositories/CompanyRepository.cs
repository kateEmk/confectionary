using Dapper;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Repository;
using LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace LibraryDatabaseCoffe.Models.DB.Request.Repositories
{
    public class CompanyRepository : AbstractRepository, IRepositiry<Company>
    {
        public const string table_name = "company";
        public const string company_id = "company_id";
        public const string company_name = "company_name";
        public const string owner = "owner";
        public const string telephone = "telephone";
        public const string banking_account = "banking_account";
        public CompanyRepository(IDapperConnectionProvider connectiomProvider) : base(connectiomProvider)
        {
        }

        public async Task AddAsync(Company record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Company>($"INSERT INTO {table_name} ( {company_name}, {owner}, {telephone}, {banking_account}) VALUES ( @{company_name}, @{owner}, @{telephone}, @{banking_account});", new { company_name = record.CompanyName, owner = record.Owner, telephone = record.Telephone, banking_account = record.BankingAccount });
        }        
        public async Task<Company> GetAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<Company>($"SELECT * FROM {table_name} WHERE {company_id} = @{company_id}", new { company_id = id});
        }
        
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<Company>($"SELECT * FROM {table_name}");
        }

        public async Task UpdateAsync(int id, Company record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Company>($"UPDATE {table_name} SET {company_name} = @{company_name}, {owner} = @{owner}, {telephone} = {telephone}, {banking_account} = @{banking_account} WHERE {company_id} = @{company_id};",new {company_name = record.CompanyName, owner = record.Owner,telephone = record.Telephone,banking_account = record.BankingAccount,company_id = id });
            return;
        }

        public async Task DeleteAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Company>($"DELETE FROM {table_name} WHERE {company_id} = @{company_id};", new { company_id = id});
            return;
        }
        public async Task<IEnumerable<int>> GetAllId()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<int>($"SELECT {company_id} FROM {table_name}");            
        }
    }
}
