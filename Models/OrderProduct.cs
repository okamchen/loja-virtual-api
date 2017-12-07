namespace loja_virtual.Models
{
  public class OrderProduct
  {
    public int Id { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
    public Price Price { get; set; }
  }
}