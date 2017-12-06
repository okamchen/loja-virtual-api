using System.Linq;
using loja_virtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace loja_virtual.Controllers
{
  [Route("api/[controller]")]
  public class PedidoController : Controller
  {
    private LojaVirtualContext context;

    public PedidoController(LojaVirtualContext context)
    {
      this.context = context;
    }

    public IActionResult Get()
    {
      return Ok(context.Produto.ToList());
    }
  }
}
