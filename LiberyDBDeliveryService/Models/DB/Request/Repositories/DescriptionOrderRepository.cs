using Dapper;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Repository;
using LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace LibraryDatabaseCoffe.Models.DB.Request.Repositories
{
    public class DescriptionOrderRepository : AbstractRepository, IRepositiry<DescriptionOrder>
    {
        public const string table_name = "description_order";
        public const string order_id = "order_id";
        public const string staff_id = "staff_id";
        public const string amount = "amount";
        public const string description_id = "description_id";
        public const string user_id = "user_id";
        public DescriptionOrderRepository(IDapperConnectionProvider connectiomProvider) : base(connectiomProvider)
        {
        }

        public async Task AddAsync(DescriptionOrder record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<DescriptionOrder>($"INSERT INTO {table_name}({user_id}, {staff_id}, {amount}) VALUES(@{user_id},@{staff_id},@{amount});", new { user_id = record.UserId, staff_id = record.StaffId, amount = record.AmountSweetStaff });
            return;
        }

        public async Task<IEnumerable<DescriptionOrder>> GetBascket(int id_user)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            string sql = $"SELECT * FROM {table_name} LEFT JOIN {SweetStaffRepository.table_name} ON {table_name}.{staff_id} = {SweetStaffRepository.table_name}.{staff_id} WHERE {table_name}.{user_id} = @{UserRepository.user_id} and {table_name}.{order_id} is null;";


            return await conn.QueryAsync<DescriptionOrder, SweetStaff, DescriptionOrder>(
                        sql,
                        (desc, staff) =>
                        {
                            desc.SweetStaff = staff;
                            return desc;
                        },
                        splitOn: "staff_id",
                        param: new { user_id = id_user });
        }

        public async Task<DescriptionOrder> GetAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<DescriptionOrder>($"SELECT * FROM {table_name} WHERE {description_id} = @{description_id};", new {description_id = id });
        }

        public async Task<IEnumerable<DescriptionOrder>> GetAllAsync()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<DescriptionOrder>($"SELECT * FROM {table_name};");
        }

        public async Task UpdateAsync(int id, DescriptionOrder record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<DescriptionOrder>($"UPDATE {table_name} SET {order_id} = @{order_id}, {staff_id} = @{staff_id}, {amount} = @{amount} WHERE {description_id} = @{description_id};", new { order_id = record.Orderid, staff_id = record.StaffId, amount = record.AmountSweetStaff, description_id = id, });
            return;
        }
        public async Task<bool> Search(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            var exist = await conn.QuerySingleAsync<bool>($"SELECT exists(SELECT * FROM {table_name} WHERE {description_id} = @{description_id});", new { description_id = id, });
            return exist;
        }
        public async Task DeleteAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<DescriptionOrder>($"DELETE FROM {table_name} WHERE {description_id} = @{description_id}",new { description_id = id });
            return;
        }
    }
}
