using System.ComponentModel.DataAnnotations;
using loja_virtual.Models;
using loja_virtual.ViewModel.Validation;

namespace loja_virtual.ViewModel
{
  public class CreateUserViewModel : UserViewModel
  {
    public CreateUserViewModel()
    {
    }

    public CreateUserViewModel(User u) : base(u)
    {
    }

    [RequiredField("Password")]
    [MinFieldLength("Password", 6)]
    
    public string Password { get; set; }

  }
}