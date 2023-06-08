using Dapper;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Repository;
using LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace LibraryDatabaseCoffe.Models.DB.Request.Repositories
{
    public class OrderRepository : AbstractRepository,IRepositiry<Order>
    {
        public const string table_name = "orders";
        public const string user_id = "user_id";
        public const string status_order = "status_order";
        public const string order_id = "order_id";
        public const string order_date = "order_date";
        public const string total = "total";
        public OrderRepository(IDapperConnectionProvider connectiomProvider) : base(connectiomProvider)
        {
        }

        public async Task AddAsync(Order order)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Order>($"INSERT INTO {table_name} ({user_id},{status_order}) VALUES (@{user_id},@{status_order});",new {user_id = order.UserId, status_order = (short)order.StatusOrder});
            return;
        }


        public async Task<Order> GetAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<Order>($"SELECT * FROM {table_name} WHERE {order_id} = @{order_id};", new { order_id = id });
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<Order>($"SELECT * FROM {table_name};");
        }

        public async Task UpdateAsync(int id, Order record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Order>($"UPDATE {table_name} SET {order_date} = @{order_date}, {status_order} = @{status_order} WHERE {order_id} = @{order_id};", new {  order_date = record.DateOrder, status_order = (short)record.StatusOrder,  order_id = id});
            return;
        }

        public async Task DeleteAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<Order>($"DELETE FROM {table_name} WHERE {order_id} = @{order_id};", new {order_id = id });
            return;
        }
    }
}
