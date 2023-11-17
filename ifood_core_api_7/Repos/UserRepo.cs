using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using Microsoft.EntityFrameworkCore;

namespace ifood_core_api_7.Repos
{
    public class UserRepo : GenericRepository<User>, IUserRepository
    {
        public UserRepo(MyDBContext dBContext) : base(dBContext) { }

        public override Task<List<User>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public override async Task<bool> Insert(User user)
        {
            try
            {
                var data = await _dbSet.AddAsync(user);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override async Task<bool> Update(User user)
        {
            try
            {
                var data = await _dbSet.Where(p=>p.Id== user.Id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.Fullname = user.Fullname;
                    data.Password = user.Password;
                    data.Email = user.Email;
                    data.Phone = user.Phone;
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {

            User data = await _dbSet.Where(p=>p.Id==id).FirstOrDefaultAsync();
            if (data == null) return false;
            _dbSet.Remove(data);
            return true;


        }
    }
}
