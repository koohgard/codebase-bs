namespace Abstraction.Command.Book.UpdateStock;

public class UpdateStockCommandResult
{
    public int BookId { get; set; }
    public int Stock { get; set; }
    public string BookTitle { get; set; }
}
