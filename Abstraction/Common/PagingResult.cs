namespace Abstraction.Command;

public class PagingResult<T> where T : class
{
    public IEnumerable<T> Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount => TotalCount / PageSize;

}
