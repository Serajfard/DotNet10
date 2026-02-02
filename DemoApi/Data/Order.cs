namespace DemoApi.Data;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }   // FK
    public User User { get; set; } = null!;

    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
