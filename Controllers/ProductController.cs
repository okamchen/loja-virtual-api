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

    public ProductController(LojaVirtualContext context) : base(context) 
    {
      if (context.Product.Count() == 0)
      {
          if (context.Category.Count() == 0)
          {
              context.Category.Add(new Category{ Id = 1, Name = "Smarthphone"});
              context.Category.Add(new Category{ Id = 2, Name = "Acessórios"});
              context.Category.Add(new Category{ Id = 3, Name = "Consoles"});
              context.Category.Add(new Category{ Id = 4, Name = "Games"});

              context.SaveChanges();
          }
      
          context.Product.Add(new Product(){
            Name = "Galaxy J7 Prime", 
            Description = "Smartphone Samsung Galaxy J7 Prime Dual Chip Android Tela 5.5' 32GB 4G Câmera 13MP - Dourado",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/129543/9/129543938SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 1},
            CategoryId = 1,
            Price = new decimal (1987.9)
          });

          context.Product.Add(new Product(){
            Name = "Moto Z2 Play", 
            Description = "Smartphone Motorola Moto Z2 Play Dual Chip Android 7.1.1 Nougat Tela 5,5' Octa-Core 2.2 GHz 64GB Câmera 12MP - Platinum",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132221/7/132221770_1GG.png",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 1},
            CategoryId = 1,
            Price = new decimal (1447)
          });

          context.Product.Add(new Product(){
            Name = "Moto X4", 
            Description = "Smartphone Motorola Moto X4 Dual Cam Android 7.0 Tela 5.2' Octa-Core 32GB Wi-Fi 4G Câmera 12MP - Preto",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132569/2/132569266SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 1},
            CategoryId = 1,
            Price = new decimal (1399)
          });

          context.Product.Add(new Product(){
            Name = "Moto G 5s Plus", 
            Description = "Smartphone Motorola Moto G 5s Plus Dual Chip Android 7.1.1 Nougat Tela 5.5' Snapdragon 625 32GB 4G 13MP Câmera Dupla - Azul Topázio",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132444/7/132444754_1GG.png",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 1},
            CategoryId = 1,
            Price = new decimal (1249.9)
          });

          context.Product.Add(new Product(){
            Name = "iPhone X", 
            Description = "iPhone X Cinza Espacial 64GB Tela 5.8' IOS 11 4G Wi-Fi Câmera 12MP - Apple",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132723/7/132723729SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 1},
            CategoryId = 1,
            Price = new decimal (6999.9)
          });

          context.Product.Add(new Product(){
            Name = "Playstation 4", 
            Description = "Console Playstation 4 Slim 500gb + 4 Jogos",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/sku/29691/2/29691263_1SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 3},
            CategoryId = 3,
            Price = new decimal (1999.9)
          });

          context.Product.Add(new Product(){
            Name = "Resident Evil 7", 
            Description = "Game Resident Evil 7 Biohazard Gold Edition - PS4",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132797/1/132797162SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 4},
            CategoryId = 4,
            Price = new decimal (179.9)
          });

          context.Product.Add(new Product(){
            Name = "UFC 3", 
            Description = "Game UFC 3 - PS4",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132886/7/132886704SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 4},
            CategoryId = 4,
            Price = new decimal (249.9)
          });

          context.Product.Add(new Product(){
            Name = "Xbox One X", 
            Description = "Console Xbox One X 1TB - Project Scorpio Edition + Controle sem Fio",
            ImageUrl = "https://images-americanas.b2w.io/produtos/01/00/item/132762/5/132762592SZ.jpg",
            ExpirationDate = DateTime.Now,
            Category = new Category(){Id = 3},
            CategoryId = 3,
            Price = new decimal (3999.9)
          });
          
          context.SaveChanges();
      }
    }

    [HttpGet("filter/{idCategoria}/{order}")]
    public IActionResult GetWithFilter(long idCategoria, string order)
    {
      var products = context.Product.Where(pdt => pdt.Category.Id == idCategoria || idCategoria == 0)
          .Include(p => p.Category).Include(p => p.HistoricPrice);
      
      order = string.IsNullOrEmpty(order) ? "" : order;
      if (order.ToLower() == "asc")
        return Ok(products.OrderBy(p => p.Price));
      else if (order.ToLower() == "desc")
        return Ok(products.OrderByDescending(p => p.Price));
      else
        return Ok(products.OrderBy(p => p.Id));

    } 

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
      context.Entry<Price>(p.HistoricPrice.FirstOrDefault()).State = EntityState.Added;
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