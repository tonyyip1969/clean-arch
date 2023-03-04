namespace CleanArch.Application.AggregatesModel.OrderAggregates;

/// <summary>
/// Product
/// </summary>
public class Product
{
     public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    private Product(string productName, decimal price)
    {
        Id = Guid.NewGuid();
        Name = productName;
        Price = price;
    }

    public static Product NewProduct(
        string productName,
        decimal price) => new Product(productName, price);
}