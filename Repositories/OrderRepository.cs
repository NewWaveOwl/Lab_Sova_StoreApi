using Lab00_Sova.Models;
using System.Text.Json;

namespace Lab00_Sova.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly string filePath;

        public OrderRepository(IWebHostEnvironment environment)
        {
            filePath = Path.Combine(environment.ContentRootPath, "Data", "orders.json");
        }

        public async Task<List<Order>> GetAllAsync()
        {
            if (!File.Exists(filePath)) return new List<Order>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var orders = await GetAllAsync();
            return orders.FirstOrDefault(order => order.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            var orders = await GetAllAsync();
            orders.Add(order);
            var json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }
    }



}
