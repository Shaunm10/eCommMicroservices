using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        
        Task<Product> GetProduct(string id);

        Task<Product> GetProductByName(string name);

        Task<Product> GetProductByCategory(string categoryName);
        

        Task CreateProduct(Product product);
        
        Task<bool> UpdateProduct(Product product);
        
        Task<bool> DeleteProduct(string id);
    }
}
