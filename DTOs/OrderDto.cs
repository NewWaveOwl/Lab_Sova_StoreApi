namespace Lab00_Sova.DTOs
{
    public sealed record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);
    public sealed record OrderDto(Guid Id, string CustomerName, string Status, DateTime CreatedAt, List<OrderItemDto> Items);
    public sealed record CreateOrderItemRequest(Guid ProductId, int Quantity);
    public sealed record CreateOrderRequest(string CustomerName, List<CreateOrderItemRequest> Items);

}
