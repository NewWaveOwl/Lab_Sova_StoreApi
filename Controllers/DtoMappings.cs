using Lab00_Sova.DTOs;
using Lab00_Sova.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab00_Sova.Controllers
{
    public static class DtoMappings
    {
        public static ProductDto ToDto(this Product product) =>
            new(product.Id, product.Name, product.Price, product.Category);

        public static OrderDto ToDto(this Order order) =>
            new(
                order.Id,
                order.CustomerName,
                order.Status,
                order.CreatedAt,
                order.Items.Select(item => new OrderItemDto(
                    item.ProductId, item.Quantity, item.UnitPrice)).ToList());
    }
}
