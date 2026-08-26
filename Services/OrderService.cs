using Lab00_Sova.DTOs;
using Lab00_Sova.Models;
using Lab00_Sova.Repositories;

namespace Lab00_Sova.Services
{
    public sealed class OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository) : IOrderService
    {
        public Task<List<Order>> GetAllAsync() => orderRepository.GetAllAsync();
        public Task<Order?> GetByIdAsync(Guid id) => orderRepository.GetByIdAsync(id);

        public async Task<Order> CreateAsync(CreateOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName))
                throw new InvalidOperationException("Customer name is required.");
            if (request.Items is null || request.Items.Count == 0)
                throw new InvalidOperationException("An order requires at least one item.");

            var items = new List<OrderItem>();
            foreach (var requestItem in request.Items)
            {
                if (requestItem.Quantity <= 0)
                    throw new InvalidOperationException("Item quantity must be greater than zero.");

                var product = await productRepository.GetByIdAsync(requestItem.ProductId);
                if (product is null)
                    throw new InvalidOperationException($"Product {requestItem.ProductId} does not exist.");

                items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = requestItem.Quantity,
                    UnitPrice = product.Price
                });
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName.Trim(),
                CreatedAt = DateTime.UtcNow,
                Items = items
            };
            await orderRepository.AddAsync(order);
            return order;
        }
    }



}
