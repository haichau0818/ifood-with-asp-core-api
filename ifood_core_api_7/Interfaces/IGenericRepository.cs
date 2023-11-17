namespace ifood_core_api_7.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid Id);

        Task<bool> Insert(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(Guid Id);


    }
}
