using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using loja_virtual.Helpers;
using loja_virtual.Models;
using loja_virtual.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace loja_virtual.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(LojaVirtualContext context) : base(context) 
        {
            if (context.User.Count() == 0)
            {
                context.User.Add(new User { 
                    Nome = "Oelton" , 
                    Email = "oelton@gmail.com",
                    Login =  "oelton",
                    Password = "1234",
                    Tipo = "admin"
                });

                context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = context.User
            .Select(p => new UserViewModel(p)).ToList();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var user = context.User.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserViewModel model)
        {
            ValidateModel();

            User user = context.User
            .FirstOrDefault(u => u.Email == model.Email || u.Login == model.Login);

            validateUser(user, model);

            context.User.Add(new User(model));
            context.SaveChanges();

            return Ok();
        }

        public void validateUser(User user, UserViewModel model)
        {
            if (user != null)
            {
                validatePermissionUser(user);

                List<string> errors = new List<string>();
                if (user.Login == model.Login)
                    errors.Add("O 'Login' informado já está sendo usado.");
                if (user.Email == model.Email)
                    errors.Add("O 'Email' informado já está sendo usado.");
                throw new ValidateException() { Errors = errors };
            }

        }

        private void validatePermissionUser(User user)
        {
            if (user?.Tipo?.ToUpper() == "ADMIN" && context.User.FirstOrDefault(u => u.Id != user.Id && u.Tipo.ToUpper() == "ADMIN") == null)
                throw new ValidateException("O único administrador não pode ser removido.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] UserViewModel model)
        {

            if (model == null || model.Id != id)
            {
                return BadRequest();
            } 
            
            User user = context.User.FirstOrDefault(u => u.Id == id);
            
            if (user == null)
            {
                return NotFound();
            } else { 
                validateUser(user, model);
            }

            user.Nome = user.Nome;
            user.Login = user.Login;
            user.Tipo = user.Tipo;
            user.Email = user.Email;
            user.Password = user.Password;

            context.User.Update(user);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = context.User.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            validatePermissionUser(user);

            context.User.Remove(user);
            context.SaveChanges();

            return Ok();
        }

    }

}