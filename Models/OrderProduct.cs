using System.Collections.Generic;

namespace loja_virtual.Models
{
  public class OrderProduct
  {
    public int Id { get; set; }

    public long IdProduct { get; set; }
    public Product Product { get; set; }
    public long IdOrder { get; set; }
    public Order Order { get; set; }
    public User Client { get; set; }
    public long IdClient { get; set; }
    public decimal Price { get; set; }
    public OrderProduct() {}
    public OrderProduct(Order order, Product product)
    {
      IdOrder = order.Id;
      IdProduct = product.Id;
      Price = product.Price;
    }
  }
}