namespace loja_virtual.Models
{
  public class VendaProduto
  {
    public int Id { get; set; }
    public Produto Produto { get; set; }
    public Venda Venda { get; set; }
    public Preco Preco { get; set; }
  }
}