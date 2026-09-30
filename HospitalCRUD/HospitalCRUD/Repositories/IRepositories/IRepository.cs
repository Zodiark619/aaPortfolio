namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);


        Task<int> SaveChangesAsync();

       // Task<List<T>> GetAllPaginationAsync(int page, int pageSize);
     //   Task<int> GetCountAsync();
    }
}
