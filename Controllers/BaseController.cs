using System.Linq;
using loja_virtual.Helpers;
using loja_virtual.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace loja_virtual.Controllers
{
  public abstract class BaseController : Controller
  {
    protected LojaVirtualContext context;
    public BaseController(LojaVirtualContext context) => this.context = context;

    protected void ValidateModel()
    {
      if (!ModelState.IsValid)
        throw new ValidateException()
        {
          Errors = ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage)
        };
    }
  }
}