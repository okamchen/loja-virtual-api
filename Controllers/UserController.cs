using System.Collections.Generic;
using System.Linq;
using loja_virtual.Helpers;
using loja_virtual.Models;
using loja_virtual.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
                    Email = "oelton@gmail.com",
                    Login =  "oelton",
                    Password = "123456",
                    Profile = "ADMIN",
                    Name = "Oelton Kamchen"
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

        [HttpPost("login")]
        public IActionResult ValidateLogin([FromBody] LoginViewModel model)
        {
            User user = context.User
                .FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
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

            User newUser = new User(model);
            context.User.Add(newUser);
            context.SaveChanges();

            return Ok(new UserViewModel(newUser));
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
            if (user?.Profile?.ToUpper() == "ADMIN" && context.User.FirstOrDefault(u => u.Id != user.Id && u.Profile.ToUpper() == "ADMIN") == null)
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
                validatePermissionUser(user);
            }

            user.Name = user.Name;
            user.Login = user.Login;
            user.Profile = user.Profile;
            user.Email = user.Email;
            user.Password = user.Password;

            context.User.Update(user);
            context.SaveChanges();

            return Ok(user);
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