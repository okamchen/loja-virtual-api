using System;
using System.Collections.Generic;
using System.Linq;

namespace loja_virtual.Models
{
  public class Preco
  {
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
  }
}