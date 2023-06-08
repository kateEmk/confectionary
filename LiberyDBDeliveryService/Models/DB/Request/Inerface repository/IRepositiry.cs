namespace LibraryDatabaseCoffe.Models.DB.Repository
{
    public interface IRepositiry<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T record);
        Task DeleteAsync(int id);
        Task UpdateAsync(int  id, T record);
        Task<T> GetAsync(int id);
    }
}
