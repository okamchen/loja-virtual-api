using System;
using System.Collections.Generic;
using loja_virtual.Models;
using loja_virtual.ViewModel.Validation;

namespace loja_virtual.ViewModel
{
  public class OrderViewModel
  {
    public int Id { get; set; }
    public User Client { get; set; }
    public string Situation { get; set; }
    public DateTime DateExpedition { get; set; }
    public DateTime DateAccepted { get; set; }
    public List<ProductViewModel> Products { get; set; }
    
  }
}