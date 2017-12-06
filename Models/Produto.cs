using System;
using System.Collections.Generic;
using System.Linq;
using loja_virtual.ViewModel;

namespace loja_virtual.Models
{
  public class Produto
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Preco> HistoricoPreco { get; set; }
    public DateTime DataValidade { get; set; }
    public Categoria Categoria { get; set; }

    public decimal Preco
    {
      get
      {
        return HistoricoPreco.Any() ? HistoricoPreco.OrderByDescending(p => p.Data).First().Valor : 0;
      }
    }
    public Produto() { }
    public Produto(ProdutoViewModel model)
    {
      Id = model.Id;
      Nome = model.Nome;
      HistoricoPreco = new List<Preco>()
      {
        new Preco() { Data = DateTime.Now, Valor = model.Preco }
      };
      DataValidade = model.DataValidade;
      Categoria = model.Categoria;
    }

  }
}