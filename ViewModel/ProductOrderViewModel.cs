using System;
using loja_virtual.Models;
using loja_virtual.ViewModel.Validation;

namespace loja_virtual.ViewModel
{
  public class ProductOrderViewModel
  {
    public long Id { get; set; }

    [RequiredField("Name")]
    public string Name { get; set; }

    [RequiredField("ImageUrl")]
    public string ImageUrl { get; set; }

    [RequiredField("Price")]
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }

    [RequiredField("Category")]
    public Category Category { get; set; }
    public string Description { get; set; }
    public ProductOrderViewModel() {}
    public ProductOrderViewModel(Product product) 
    {
      Id = product.Id;
      Name = product.Name;
      ImageUrl = product.ImageUrl;
      Price = product.Price;
      ExpirationDate = product.ExpirationDate;
      Category = product.Category;
      Description = product.Description;
    }

  }
}
