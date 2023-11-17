using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using Microsoft.EntityFrameworkCore;

namespace ifood_core_api_7.Repos
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected MyDBContext _dbContext;

        protected DbSet<T> _dbSet;
        public GenericRepository(MyDBContext dbContext) {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }

    

        public virtual Task<List<T>> GetAllAsync()
        {
            return this._dbSet.ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();

        }

        public virtual Task<bool> Insert(T model)
        {
            throw new NotImplementedException();

        }

        public virtual Task<bool> Update(T model)
        {
            throw new NotImplementedException();
        }
        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
