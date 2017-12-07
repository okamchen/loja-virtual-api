using System.Linq;
using loja_virtual.Models;
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
  }
}
