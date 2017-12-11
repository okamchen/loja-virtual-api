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


    [HttpGet("detail/{idOrder}")]
    public IActionResult GetDetail(long idOrder)
    {

      if(idOrder <= 0) 
      {
        throw new ValidateException("Unidentified order");
      }

      Order order = context.Order.FirstOrDefault(o=> o.Id == idOrder);
      List<ProductOrderViewModel> products = new List<ProductOrderViewModel>();
      
      var ordersProductsModel = context.OrderProduct.Where(op=> op.IdOrder == order.Id).ToList(); 
      UserViewModel client = null;

      List<ProductOrderViewModel> productsOrderView = new List<ProductOrderViewModel>();
      ordersProductsModel.ForEach(opm=> {
        var product = context.Product.FirstOrDefault(pdt => pdt.Id == opm.IdProduct);
        productsOrderView.Add(new ProductOrderViewModel(product));

        var clientModel = context.User.FirstOrDefault(u => u.Id == opm.IdClient);
        client = new UserViewModel(clientModel);
      });

      return Ok(new OrderViewModel(order, productsOrderView, client));
    }

    [HttpGet("{idUser}")]
    public IActionResult Get(long idUser)
    {
      var user = context.User.Where(u => u.Id == idUser).FirstOrDefault();
      if( user == null || user.Id <= 0) 
      {
        throw new ValidateException("Unidentified user");
      }

      IQueryable<Order> order = context.Order;

      if (user.Profile != "ADMIN")
      {
        order = order.Where(p => p.IdClient == user.Id);
      }

      var orders = new List<OrderViewModel>();
      List<ProductOrderViewModel> products = new List<ProductOrderViewModel>();

      var ordersModel = order.ToList();
      
      List<OrderViewModel> ordersView = new List<OrderViewModel>();
      foreach (Order ord in ordersModel)
      {
        var ordersProductsModel = context.OrderProduct.Where(op=> op.IdOrder == ord.Id).ToList(); 
        UserViewModel client = null;

        List<ProductOrderViewModel> productsOrderView = new List<ProductOrderViewModel>();
        ordersProductsModel.ForEach(opm=> {
          var product = context.Product.FirstOrDefault(pdt => pdt.Id == opm.IdProduct);
          productsOrderView.Add(new ProductOrderViewModel(product));

          var clientModel = context.User.FirstOrDefault(u => u.Id == opm.IdClient);
          client = new UserViewModel(clientModel);
        });

        ordersView.Add(new OrderViewModel(ord, productsOrderView, client));

      }

      return Ok(ordersView);
    }

    [HttpGet("product")]
    public IActionResult GetProductOrder(UserViewModel modelUser)
    {
      var orders = context.OrderProduct.ToList();
      return Ok(orders);
    }

    [HttpPost]
    public IActionResult Post([FromBody] OrderViewModel model)
    {
      ValidateModel();

      var order = new Order(model);
      context.Order.Add(order);
      context.SaveChanges();

      List<OrderProduct> orderProducts = new List<OrderProduct>();
      decimal totalPrice = new decimal(0);
      foreach (ProductOrderViewModel modelProduct in model.Products) 
      {
        var op = new OrderProduct() {IdOrder = order.Id, IdProduct = modelProduct.Id, IdClient = model.Client.Id};
        orderProducts.Add(op);
        totalPrice =+ modelProduct.Price;
      }

      order.TotalPrice = totalPrice;
      order.Situation = "Pending";

      orderProducts.ForEach(op => context.OrderProduct.Add(op));
      context.SaveChanges();

      return Ok(order);
    }
  }
}
