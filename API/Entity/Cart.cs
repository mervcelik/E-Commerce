namespace API.Entity;

public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; } = null!;
    public List<CartItem> CartItems { get; set; } = new();

    public void AddItem(Product product, int quantity)
    {
        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            CartItems.Add(new CartItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            });
        }
    }

    public void DeleteItem(int productId,int quantity)
    {
        var item = CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
           item.Quantity -= quantity;
        }

        if (item != null && item.Quantity <= 0)
        {
            CartItems.Remove(item);
        }
    }
}

public class CartItem
{
    public int CartItemId { get; set; }
    public int CartId { get; set; }
  //  public Cart Cart { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}