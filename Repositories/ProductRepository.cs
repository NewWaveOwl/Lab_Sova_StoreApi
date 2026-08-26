using Lab00_Sova.Models;
using System.Text.Json;

namespace Lab00_Sova.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly string filePath;

        public ProductRepository(IWebHostEnvironment environment)
        {
            filePath = Path.Combine(environment.ContentRootPath, "Data", "products.json");
        }

        public async Task<List<Product>> GetAllAsync()
        {
            if (!File.Exists(filePath)) return new List<Product>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var products = await GetAllAsync();
            return products.FirstOrDefault(product => product.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            var products = await GetAllAsync();
            products.Add(product);
            await SaveAsync(products);
        }

        public async Task UpdateAsync(Product product)
        {
            var products = await GetAllAsync();
            var index = products.FindIndex(existing => existing.Id == product.Id);
            if (index < 0) return;

            products[index] = product;
            await SaveAsync(products);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var products = await GetAllAsync();
            var removed = products.RemoveAll(product => product.Id == id) > 0;
            if (removed) await SaveAsync(products);
            return removed;
        }

        private async Task SaveAsync(List<Product> products)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(products, options);
            await File.WriteAllTextAsync(filePath, json);
        }

    }
}
