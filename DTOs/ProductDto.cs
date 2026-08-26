namespace Lab00_Sova.DTOs
{
public sealed record ProductDto(Guid Id, string Name, decimal Price, string Category);
public sealed record CreateProductRequest(string Name, decimal Price, string Category);
public sealed record UpdateProductRequest(string Name, decimal Price, string Category);



}
