using Lab00_Sova.DTOs;
using Lab00_Sova.Models;

namespace Lab00_Sova.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> CreateAsync(CreateOrderRequest request);
    }
}
