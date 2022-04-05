using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var deletedResult = await _context.Products
                .DeleteOneAsync(filter);

            return deletedResult.IsAcknowledged &&
                deletedResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

            return await _context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);

            return await _context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .Find(x => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);


            var updateResult = await _context.Products
                .ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && 
                updateResult.ModifiedCount > 0;
        }
    }
}
