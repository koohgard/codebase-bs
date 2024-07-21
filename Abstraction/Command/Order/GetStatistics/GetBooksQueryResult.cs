
namespace Abstraction.Command.Order.GetStatistics;
public class GetOrderStatisticsQueryResult
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int TotalOrderCount { get; set; }
    public int TotalBookCount { get; set; }
    public decimal TotalPurchaseAmount { get; set; }
}
