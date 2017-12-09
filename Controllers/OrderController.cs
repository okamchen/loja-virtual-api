using System.Linq;
using loja_virtual.Helpers;
using loja_virtual.Models;
using loja_virtual.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace loja_virtual.Controllers
{
  [Route("api/[controller]")]
  public class OrderController : BaseController
  {
    public OrderController(LojaVirtualContext context) : base(context) { }

    public IActionResult Get()
    {
      return Ok(context.Product.ToList());
    }


    [HttpPost]
    public IActionResult Post([FromBody] OrderViewModel model)
    {
      ValidateModel();

      var order = new Order(model);
      context.Order.Add(order);

      foreach (ProductViewModel modelProduct in model.Products) 
      {
        var op = new OrderProduct() {Order = order, Product = new Product(modelProduct)};
        context.OrderProduct.Add(op);
      }

      context.SaveChanges();
      return Ok(order);
    }
  }
}
