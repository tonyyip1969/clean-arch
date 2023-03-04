namespace CleanArch.Application.AggregatesModel.OrderAggregates;

/// <summary>
/// Aggregate root
/// </summary>
public class Order
{
    private Order(
        string firstName,
        string lastName,
        Address shippingAddress)
    {
        Id = Guid.NewGuid();
        Customer = 
            Customer.NewCustomer(firstName, lastName, shippingAddress);
    }

    public Guid Id { get; private set; }
    public Customer Customer { get; private set; }
    private List<OrderItem> _orderItems = new List<OrderItem>();
    public decimal TotalCost => 
        OrderItems.Sum(x => x.Cost);
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public void AddItem(
        string productName,
        decimal productPrice,
        int quantity)
    {
        var existantItem = _orderItems
            .FirstOrDefault(x => x.Product.Name == productName &&
                                    x.Product.Price == productPrice);
        
        if (existantItem is not null)
        {
            existantItem.UpdateQuantity(quantity);
            return;
        }                                

        var product = Product.NewProduct(productName, productPrice);
        var orderItem = OrderItem.NewOrderItem(product, quantity);
        _orderItems.Add(orderItem);

        Validate();
    }

    public void RemoveOrderItem(Guid itemId)
    {
        var item = _orderItems.FirstOrDefault(item => item.Id == itemId);

        if (item is null)
            throw new OrderItemNotFoundException();

        _orderItems.Remove(item);

        Validate();
    }

    public void UpdateItemQuantity(Guid itemId, int quantity)
    {
        var item = _orderItems.FirstOrDefault(_ => _.Id == itemId);

        if (item is null)
            throw new OrderItemNotFoundException();

        item.UpdateQuantity(quantity);

        Validate();
    }

    public Dictionary<string, string>? Errors { get; set; }

    public static Order NewOrder(
        string firstName, 
        string lastName, 
        Address shippingAddress)
    {
        Order instance = new(firstName, lastName, shippingAddress);

        instance.Validate();

        return instance;
    }

    private void Validate()
    {
        // TODO: Here we need to validate if all the Order aggregate is valid
        //       this method need to be called in every public method, 
        //       after change the aggregate
    }
}