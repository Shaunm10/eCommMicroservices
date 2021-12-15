namespace Catalog.Api.Repositories;

using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

public class ProductRepository : IProductRepository
{
    /// <summary>
    /// The MongoDb Driver
    /// </summary>
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Commands:
    public async Task CreateProduct(Product product)
    {
        await this._context.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await this._context.Products.DeleteOneAsync(filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    // Queries:
    public async Task<Product?> GetProduct(string id)
    {
        return await this._context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await this._context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await this._context
            .Products.Find(p => true).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await this._context.Products.ReplaceOneAsync(p => p.Id == product.Id, replacement: product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await this._context
            .Products
            .Find(filter)
            .ToListAsync();
    }
}