using Dapper;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Repository;
using LibraryDatabaseCoffe.Models.DB.Request.AbstractRepositorys;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace LibraryDatabaseCoffe.Models.DB.Request.Repositories
{
    public class SweetStaffRepository : AbstractRepository, IRepositiry<SweetStaff>
    {
        public const string table_name = "sweet_staff";
        public const string staff_name = "staff_name";
        public const string date_receipt = "date_receipt";
        public const string company_id = "company_id";
        public const string weight = "weight";
        public const string classification = "classification";
        public const string price = "price";
        public const string calories = "calories";
        public const string staff_id = "staff_id";
        public SweetStaffRepository(IDapperConnectionProvider connectiomProvider) : base(connectiomProvider)
        {
        }

        public async Task AddAsync(SweetStaff record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<SweetStaff>($"INSERT INTO {table_name} ({staff_name}, {date_receipt}, {company_id}, {weight}, {classification}, {price}, {calories}) VALUES (@{staff_name}, @{date_receipt}, @{company_id}, @{weight}, @{classification}, @{price}, @{calories});", new { staff_name = record.StaffName, date_receipt = record.DateDeliver, company_id = record.CompanyId, weight = record.Weight, classification = record.Classification, price = record.Price, calories = record.Calories });
            return;
        }

        public async Task DeleteAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<SweetStaff>($"DELETE FROM {table_name} WHERE {staff_id} = @{staff_id};", new { staff_id = id });
            return;
        }

        public async Task<SweetStaff> GetAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QuerySingleAsync<SweetStaff>($"SELECT * FROM {table_name} WHERE {staff_id} = @{staff_id};", new { staff_id = id });
        }
        public async Task<IEnumerable<SweetStaff>> GetFullAsync(int id)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            string sql = $"SELECT * FROM {table_name} LEFT JOIN {CompanyRepository.table_name} ON {table_name}.{company_id} = {CompanyRepository.table_name}.{company_id} WHERE {table_name}.{staff_id} = @{staff_id} LIMIT 1;";


            return await conn.QueryAsync<SweetStaff, Company, SweetStaff>(
                        sql,
                        (staff, company) =>
                        {
                            staff.Company = company;
                            return staff;
                        },
                        splitOn: company_id,
                        param: new { staff_id = id });
        }
        public async Task<IEnumerable<SweetStaff>> GetAllAsync()
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<SweetStaff>($"SELECT * FROM {table_name};");
        }
        public async Task<IEnumerable<SweetStaff>> SearchByName(string productName)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<SweetStaff>($"SELECT * FROM {table_name} WHERE {staff_name} ILIKE '%{productName}%';");
        }
        public async Task UpdateAsync(int id, SweetStaff record)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            await conn.QueryAsync<SweetStaff>($"UPDATE {table_name} SET {staff_name} = @{staff_name}, {date_receipt} = @{date_receipt}, {company_id} = @{company_id}, {weight} = @{weight}, {classification} = @{classification}, {price} = @{price}, {calories} = @{calories} WHERE {staff_id} = @{staff_id};", new { staff_name = record.StaffName, date_receipt = record.DateDeliver, company_id = record.CompanyId, weight = record.Weight, classification = record.Classification, price = record.Price, calories = record.Calories, staff_id = id });
            return;
        } 

        public async Task<IEnumerable<SweetStaff>> SelectByName(string nameStaff)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<SweetStaff>($"SELECT * FROM {table_name} WHERE {staff_name} = @{staff_name};",new { staff_name = nameStaff });
        }
        public async Task<IEnumerable<SweetStaff>> SelectByNameAndId(string nameStaff, int idStaff)
        {
            await using var conn = await ConnectiomProvider.GetConnectionAsync();
            return await conn.QueryAsync<SweetStaff>($"SELECT * FROM {table_name} WHERE {staff_name} = @{staff_name} and {staff_id} = @{staff_id};", new { staff_name = nameStaff, staff_id = idStaff });
        }
    }
}
