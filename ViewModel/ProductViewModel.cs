using System;
using loja_virtual.Models;
using loja_virtual.ViewModel.Validation;

namespace loja_virtual.ViewModel
{
  public class ProductViewModel
  {
    public int Id { get; set; }

    [RequiredField("Name")]
    public string Name { get; set; }

    [RequiredField("Price")]
    public decimal Price { get; set; }

    [RequiredField("ExpirationDate")]
    public DateTime ExpirationDate { get; set; }

    [RequiredField("Category")]
    public Category Category { get; set; }
  }
}