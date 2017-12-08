using System;
using System.Collections.Generic;
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


    [HttpGet("{id}", Name = "GetProduct")]
    public IActionResult GetById(long id)
    {
        var product = context.Product.Include(p => p.Category).Include(p => p.HistoricPrice).FirstOrDefault(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return new ObjectResult(product);
    }


    [HttpPost]
    public IActionResult Post([FromBody] ProductViewModel model)
    {
      ValidateModel();

      if (model.Price <= 0)
        throw new ValidateException("Price must be greater than zero");

      var p = new Product(model);
      context.Entry<Product>(p).State = EntityState.Added;
      context.Entry<Price>(p.HistoricPrice.FirstOrDefault()).State = EntityState.Added;
      context.SaveChanges();

      return Ok(p);
    }

    [HttpPut("{id}", Name = "PutProduct")]
    public IActionResult Put(long id,[FromBody] ProductViewModel model)
    {
      ValidateModel();
      
      if (model.Price <= 0)
        throw new ValidateException("Price must be greater than zero");

      var p = new Product(model);
      context.Entry<Product>(p).State = EntityState.Modified;
      context.SaveChanges();

      return Ok(p);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (context.OrderProduct.FirstOrDefault(p => p.Product.Id == id) != null)
        throw new ValidateException("The product is linked to orders and can not be deleted.");
      
      var product = context.Product.Include(p => p.HistoricPrice).FirstOrDefault(p => p.Id == id);
      
      context.Price.RemoveRange(product.HistoricPrice);
      context.Product.Remove(product);
      context.SaveChanges();

      return Ok();
    }
  }
}