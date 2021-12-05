using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await this._context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<Product> GetProductByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await this._context
                .Products.Find(p => true).ToListAsync();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
