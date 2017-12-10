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
  public class OrderController : BaseController
  {
    public OrderController(LojaVirtualContext context) : base(context) { }

    public IActionResult Get(UserViewModel modelUser)
    {
      // return Ok(context.OrderProduct.ToList());
      IQueryable<OrderProduct> orderProduct = context.OrderProduct
        .Include(op => op.Product)
        .Include(op => op.Order);

      if (modelUser.Profile == "ADMIN")
      {
        // orderProduct = orderProduct.Where(p => p.IdOrder == Convert.ToInt32(userId));
      }

      var lastUser = new User();
      var orders = new List<OrderViewModel>();
      foreach (OrderProduct op in orderProduct.ToList()) 
      {
        var order = context.Order.Include(p => p.Client).FirstOrDefault(p => p.Id == op.IdOrder);
        op.Order = order;
        if(lastUser.Id < 0 || lastUser.Id != op.Order.Client.Id)
        {
          var orderModel = op.Order;
          var productsModel = context.Product.Include(p => op.IdProduct).ToList();

          List<ProductOrderViewModel> products = new List<ProductOrderViewModel>();
          productsModel.ForEach(pm => products.Add(new ProductOrderViewModel(pm)));

          orders.Add(new OrderViewModel(orderModel, products));
        }
        lastUser = op.Order.Client;
      }

      return Ok();
    }


    [HttpPost]
    public IActionResult Post([FromBody] OrderViewModel model)
    {
      ValidateModel();

      var order = new Order(model);

      List<OrderProduct> orderProducts = new List<OrderProduct>();
      decimal totalPrice = new decimal(0);
      foreach (ProductOrderViewModel modelProduct in model.Products) 
      {
        var op = new OrderProduct() {IdOrder = order.Id, IdProduct = modelProduct.Id};
        orderProducts.Add(op);
        totalPrice =+ modelProduct.Price;
      }

      order.TotalPrice = totalPrice;
      order.Situation = "Pending";

      context.Order.Add(order);
      orderProducts.ForEach(op => context.OrderProduct.Add(op));
      context.SaveChanges();

      return Ok(order);
    }
  }
}
