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
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Price> HistoricPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; } 
    public Product() { }
    public Product(ProductViewModel model)
    {
      Id = model.Id;
      Name = model.Name;
      ImageUrl = model.ImageUrl;
      Price = model.Price;
      HistoricPrice = new List<Price>()
      {
        new Price() { DateUpdate = DateTime.Now, Value = model.Price }
      };
      ExpirationDate = model.ExpirationDate;
      Category = model.Category;
      CategoryId = model.Category.Id;
      Description = model.Description;
    }
  }
}