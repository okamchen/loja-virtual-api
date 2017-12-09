using System;
using System.Collections.Generic;
using loja_virtual.ViewModel;

namespace loja_virtual.Models
{
  public class Order
  {
    public int Id { get; set; }
    public long IdClient { get; set; }
    public User Client { get; set; }
    public DateTime DateOrder { get; set; }
    public string Situation { get; set; }
    public DateTime DateExpedition { get; set; }
    public DateTime DateAccepted { get; set; }
    public OrderProduct OrderProduct { get; set; }
    public Price Price { get; set; }
    public Order() { }
    public Order(OrderViewModel model)
    {
      Id = model.Id;
      IdClient = model.Client.Id;
      Client = model.Client;
      DateOrder = DateTime.Now;
      Situation = model.Situation;
      DateExpedition = model.DateExpedition;
      DateAccepted = model.DateAccepted;
    }
  }
}