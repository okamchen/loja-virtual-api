using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using loja_virtual.Models;
using System.Linq;

namespace loja_virtual.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        public CategoryController(LojaVirtualContext context) : base(context) { }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return context.Category.ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(long id)
        {
            var categoria = context.Category.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return new ObjectResult(categoria);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            context.Category.Add(category);
            context.SaveChanges();

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Category category)
        {

            if (category == null || category.Id != id)
            {
                return BadRequest();
            }

            var newCategory = context.Category.FirstOrDefault(c => c.Id == id);
            if (newCategory == null)
            {
                return NotFound();
            }

            newCategory.Name = category.Name;

            context.Category.Update(newCategory);
            context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var category = context.Category.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            context.Category.Remove(category);
            context.SaveChanges();
            return new NoContentResult();
        }

    }

}