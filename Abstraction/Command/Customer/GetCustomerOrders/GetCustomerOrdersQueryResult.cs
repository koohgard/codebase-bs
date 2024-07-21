using Abstraction.Enum;

namespace Abstraction.Command.Customer.GetCustomerOrders;
public class GetCustomerOrdersQueryResult
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal BookPrice { get; set; }
    public int Count { get; set; }
    public decimal TotalPrice { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
