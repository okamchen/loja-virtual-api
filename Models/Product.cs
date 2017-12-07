using System;
using System.Collections.Generic;
using System.Linq;
using loja_virtual.ViewModel;

namespace loja_virtual.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Price> HistoricPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Category Category { get; set; }

    public decimal Price
    {
      get
      {
        return HistoricPrice.Any() ? HistoricPrice.OrderByDescending(p => p.DateUpdate).First().Amount : 0;
      }
    }
    public Product() { }
    public Product(ProductViewModel model)
    {
      Id = model.Id;
      Name = model.Name;
      HistoricPrice = new List<Price>()
      {
        new Price() { DateUpdate = DateTime.Now, Amount = model.Price }
      };
      ExpirationDate = model.ExpirationDate;
      Category = model.Category;
    }

  }
}