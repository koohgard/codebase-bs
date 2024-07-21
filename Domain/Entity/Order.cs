using Abstraction.Enum;

namespace Domain.Entity;
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
