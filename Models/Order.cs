using System;

namespace loja_virtual.Models
{
  public class Order
  {
    public int Id { get; set; }
    public int IdClient { get; set; }
    public DateTime DateOrder { get; set; }
    public string Situation { get; set; }
    public DateTime DateExpedition { get; set; }
    public DateTime DateAccepted { get; set; }
  }
}