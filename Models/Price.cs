using System;
using System.Collections.Generic;
using System.Linq;

namespace loja_virtual.Models
{
  public class Price
  {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateUpdate { get; set; }
  }
}