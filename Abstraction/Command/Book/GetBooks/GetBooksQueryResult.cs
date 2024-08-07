﻿
namespace Abstraction.Command.Book.GetBooks;
public class GetBooksQueryResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? Stock { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public bool OutOfStock { get; set; }
    public byte[] RowVersion { get; set; }
}
