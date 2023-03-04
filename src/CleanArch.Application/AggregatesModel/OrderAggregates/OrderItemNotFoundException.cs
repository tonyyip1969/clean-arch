namespace CleanArch.Application.AggregatesModel.OrderAggregates;

/// <summary>
/// Custom Exception
/// </summary>
public class OrderItemNotFoundException : Exception
{
    public OrderItemNotFoundException() : base("Order item not found.")
    {
    }
}