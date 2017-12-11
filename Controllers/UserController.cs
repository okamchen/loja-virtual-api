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
        public UserController(LojaVirtualContext context) : base(context) {}

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

            var amoutAdmin = context.User.Where(u=> u.Profile.ToUpper() == "ADMIN").ToList().Count();
            model.Profile = amoutAdmin == 0 ? "ADMIN" : "USER";

            User newUser = new User(model);
            context.User.Add(newUser);
            context.SaveChanges();

            return Ok(new UserViewModel(newUser));
        }

        public void validateUser(User user, UserViewModel model)
        {
            if (user != null)
            {
                List<string> errors = new List<string>();
                if (user.Login == model.Login)
                    errors.Add("Login is already being used.");
                if (user.Email == model.Email)
                    errors.Add("Email is already being used.");
                throw new ValidateException() { Errors = errors };
            }

        }

        private void validatePermissionUser(User user)
        {
            if (context.User.Where(u => u.Profile.ToUpper() == "ADMIN").ToList().Count() <= 1)
                throw new ValidateException("The only administrator can not be removed.");
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

            var countOrderLinked = context.OrderProduct.Where(op => op.IdClient == id).ToList().Count();
            countOrderLinked =+ context.Order.Where(o => o.IdClient == id).ToList().Count();
            if(countOrderLinked > 0) 
                throw new ValidateException("User is linked to a order.");

            context.User.Remove(user);
            context.SaveChanges();

            return Ok();
        }

    }

}