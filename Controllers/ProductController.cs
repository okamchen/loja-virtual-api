using System.Linq;
using loja_virtual.Helpers;
using loja_virtual.Models;
using loja_virtual.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace loja_virtual.Controllers
{

  [Route("api/[controller]")]
  public class ProductController : BaseController
  {

    public ProductController(LojaVirtualContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get() => Ok(context.Product.Include(p => p.Category).Include(p => p.HistoricPrice).ToList());

    [HttpPost]
    public IActionResult Post([FromBody] ProductViewModel model)
    {
      ValidateModel();

      if (model.Price <= 0)
        throw new ValidateException("Price must be greater than zero");

      var p = new Product(model);
      context.Entry<Product>(p).State = EntityState.Added;
      context.Entry<Price>(p.HistoricPrice.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    public IActionResult Put([FromBody] ProductViewModel model)
    {
      ValidateModel();

      if (model.Price <= 0)
        throw new ValidateException("Price must be greater than zero");

      var p = new Product(model);
      context.Entry<Product>(p).State = EntityState.Modified;
      context.Entry<Price>(p.HistoricPrice.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (context.OrderProduct.FirstOrDefault(p => p.Product.Id == id) != null)
        throw new ValidateException("The product is linked to orders and can not be deleted.");
      
      var product = context.Product.Include(p => p.HistoricPrice).FirstOrDefault(p => p.Id == id);
      context.Price.RemoveRange(product.HistoricPrice);
      context.Product.Remove(product);
      return Ok();
    }
  }
}