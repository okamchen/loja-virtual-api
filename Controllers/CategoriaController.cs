using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using loja_virtual.Models;
using System.Linq;

namespace loja_virtual.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly LojaVirtualContext _context;

        public CategoriaController(LojaVirtualContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Categoria> GetAll()
        {
            return _context.Categoria.ToList();
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public IActionResult GetById(long id)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return new ObjectResult(categoria);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }

            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Categoria categoria)
        {

            if (categoria == null || categoria.Id != id)
            {
                return BadRequest();
            }

            var newCategoria = _context.Categoria.FirstOrDefault(c => c.Id == id);
            if (newCategoria == null)
            {
                return NotFound();
            }

            newCategoria.Nome = categoria.Nome;

            _context.Categoria.Update(newCategoria);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }

}