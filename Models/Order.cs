namespace Lab00_Sova.Models
{
    public sealed class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }


}
