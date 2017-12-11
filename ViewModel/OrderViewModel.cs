using System;
using System.Collections.Generic;
using loja_virtual.Models;

namespace loja_virtual.ViewModel
{
  public class OrderViewModel
  {
    public int Id { get; set; }
    public UserViewModel Client { get; set; }
    public string Situation { get; set; }
    public DateTime DateExpedition { get; set; }
    public DateTime DateAccepted { get; set; }
    public List<ProductOrderViewModel> Products { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderViewModel(){}

    public OrderViewModel(Order model, List<ProductOrderViewModel> products, UserViewModel client)
    {
      Id = model.Id;
      Client = client;
      Situation = model.Situation;
      DateExpedition = model.DateExpedition;
      DateAccepted = model.DateAccepted;
      Products = products;
      TotalPrice = model.TotalPrice;
    }
    
  }
}