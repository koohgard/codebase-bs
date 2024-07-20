namespace Abstraction.Command.Book.UpdateStock;

public class UpdateStockCommandResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}
