using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;

namespace ifood_core_api_7.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _dbContext;
        public IUserRepository UserRepository { get;private set; }

        public UnitOfWork(MyDBContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository= new UserRepo(dbContext);
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
