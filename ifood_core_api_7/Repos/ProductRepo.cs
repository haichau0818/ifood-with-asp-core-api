using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using Microsoft.EntityFrameworkCore;

namespace ifood_core_api_7.Repos
{
    public class ProductRepo:GenericRepository<Product>, IProductRepository
    {
        public ProductRepo(MyDBContext dBContext) : base(dBContext) { }

        public override Task<List<Product>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public override async Task<bool> Insert(Product product)
        {
            try
            {
                var data = await _dbSet.AddAsync(product);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override async Task<bool> Update(Product product)
        {
            try
            {
                var data = await _dbSet.Where(p => p.Id == product.Id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.Name = product.Name;
                    data.Price = product.Price;
                    data.Producer= product.Producer;
                    data.ExpDate = product.ExpDate;
                    data.Description = product.Description;
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

            Product data = await _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (data == null) return false;
            _dbSet.Remove(data);
            return true;


        }

    }
}
