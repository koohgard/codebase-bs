
using Abstraction.Enum;

namespace Abstraction.Command.Order.CreateOrder;

public class CreateOrderCommandResult
{
    public int Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int BookId { get; set; }
    public int Count { get; set; }
    public string BookTitle { get; set; }
    public decimal BookPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
