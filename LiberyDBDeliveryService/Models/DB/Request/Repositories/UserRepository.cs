using Dapper;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Repository;
using LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace LibraryDatabaseCoffe.Models.DB.Request.Repositories
{
    public class UserRepository : AbstractRepository, IRepositiry<User>
    {
        public const string table_name = "users";
        public const string user_name = "user_name";
        public const string email = "email";
        public const string password = "password";
        public const string status = "status";
        public const string total = "total";
        public const string user_id = "user_id";
        public UserRepository(IDapperConnectionProvider connectiomProvider) : base(connectiomProvider)
        {
        }

        public async Task AddAsync(User record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<User>($"INSERT INTO {table_name} ( {user_name} , {email}, {password}, {status}, {total}) VALUES (@{user_name}, @{email}, @{password}, @{status}, @{total});",new { user_name = record.NameUser, email = record.EmailUser, password = record.Password, status = record.Status, total = record.TotalSpent });
            return;
        }
        public async Task<bool> SearchUserByEmail(string iEmail) 
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<bool>($"select exists (select true from {table_name} where {table_name}.{email} = @{email});",new { email = iEmail});
        }
        public async Task<bool> SearchUserByName(string name)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<bool>($"select exists (select true from {table_name} where {table_name}.{user_name} = @{user_name});", new { user_name = name });
        }
        public async Task<User?> GetUserByEmailAndPassword(string pEmail, string pPassword)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            var searchedUser = await conn.QueryAsync<User?>($"select * from {table_name} where {email} = @{email} and {password} = @{password} LIMIT 1;", new { email = pEmail, password = pPassword });
            return searchedUser.FirstOrDefault();
        }
        public async Task<User?> GetFullInfoUser(int id_user)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            string sql = $"SELECT * FROM {table_name} LEFT JOIN {OrderRepository.table_name} ON {table_name}.{user_id} = {OrderRepository.table_name}.{user_id} " +
                                                    $"LEFT JOIN {DescriptionOrderRepository.table_name} ON {OrderRepository.table_name}.{OrderRepository.order_id} = {DescriptionOrderRepository.table_name}.{DescriptionOrderRepository.order_id} " +
                                                    $"LEFT JOIN {SweetStaffRepository.table_name} ON {SweetStaffRepository.table_name}.{SweetStaffRepository.staff_id} = {DescriptionOrderRepository.table_name}.{DescriptionOrderRepository.staff_id} " +
                                                    $"WHERE {table_name}.{user_id} = @{user_id};";

            User? mainUser = null;
            List<Order> mainOrders = new List<Order>();
            List<DescriptionOrder> mainDescorders = new List<DescriptionOrder>();
            List<SweetStaff> mainStaffs = new List<SweetStaff>();

            var dataInfo = await conn.QueryAsync<User, Order, DescriptionOrder, SweetStaff, User>(
                            sql,
                            (user, orders, descOrders,staffs) =>
                            {
                                mainUser = user;
                                if (orders != null)
                                {
                                    if (mainOrders.All(x => x.OrderId != orders.OrderId)) mainOrders.Add(orders);
                                    if(descOrders != null) mainDescorders.Add(descOrders);
                                    if(staffs != null) mainStaffs.Add(staffs);
                                }
                                
                                return user;
                            },
                            splitOn: "order_id,order_id,staff_id",
                            param: new { user_id = id_user });

            if (mainUser is null) 
            {
                return null;
            }

            mainDescorders.ForEach(x => x.SweetStaff = mainStaffs.First(y => x.StaffId == y.StaffId));
            mainOrders.ForEach(x => x.DescriptionOrders = mainDescorders.Where(y => y.Orderid == x.OrderId).ToList());
            mainUser.Orders = mainOrders;

            return mainUser;
        }
        public async Task<User> GetAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<User>($"SELECT * FROM {table_name} WHERE {user_id} = @{user_id};",new {user_id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<User>($"SELECT * FROM {table_name};");
        }

        public async Task UpdateAsync(int id, User record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<User>($"UPDATE {table_name} SET {user_name} = @{user_name}, {email} = @{email}, {password} = @{password}, {status} = @{status}, {total} = @{total} WHERE {user_id} = @{user_id};",new { user_name = record.NameUser, email = record.EmailUser, password = record.Password, status = record.Status, total = record.TotalSpent , user_id  = id});
            return;
        }

        public async Task DeleteAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<User>($"DELETE FROM {table_name} WHERE {user_id} = @{user_id};",new { user_id = id});
            return;
        }
    }
}
