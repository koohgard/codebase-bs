namespace Domain.Entity;
public class StockTransaction
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Count { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionFactor TransactionFactor { get; set; }
    public DateTime CreateDateTime { get; set; }
    public virtual OrderDetail OrderDetail { get; set; }
    public virtual Book Book { get; set; }

}
