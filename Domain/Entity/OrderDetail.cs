namespace Domain.Entity;
public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int? StockTransactionId { get; set; }
    public int Count { get; set; }
    public virtual Order Order { get; set; }
    public virtual Book Book { get; set; }
    public virtual StockTransaction StockTransaction { get; set; }
}
