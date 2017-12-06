using System.ComponentModel.DataAnnotations;
using loja_virtual.Models;
using loja_virtual.ViewModel.Validation;

namespace loja_virtual.ViewModel
{
  public class UserViewModel
  {
    public UserViewModel() { }

    public UserViewModel(User u)
    {
      Id = u.Id;
      Email = u.Email;
      Login = u.Login;
      Tipo = u.Tipo;
    }
    public long Id { get; set; }

    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}