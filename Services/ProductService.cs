using Lab00_Sova.DTOs;
using Lab00_Sova.Models;
using Lab00_Sova.Repositories;

namespace Lab00_Sova.Services
{
    public sealed class ProductService(IProductRepository repository) : IProductService
    {
        public Task<List<Product>> GetAllAsync() => repository.GetAllAsync();
        public Task<Product?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);
        public Task<bool> DeleteAsync(Guid id) => repository.DeleteAsync(id);

        public async Task<Product> CreateAsync(CreateProductRequest request)
        {
            Validate(request.Name, request.Price);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Price = request.Price,
                Category = request.Category.Trim()
            };
            await repository.AddAsync(product);
            return product;
        }

        public async Task<Product?> UpdateAsync(Guid id, UpdateProductRequest request)
        {
            Validate(request.Name, request.Price);
            var product = await repository.GetByIdAsync(id);
            if (product is null) return null;

            product.Name = request.Name.Trim();
            product.Price = request.Price;
            product.Category = request.Category.Trim();
            await repository.UpdateAsync(product);
            return product;
        }

        private static void Validate(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Product name is required.");
            if (price <= 0)
                throw new InvalidOperationException("Product price must be greater than zero.");
        }
    }



}
