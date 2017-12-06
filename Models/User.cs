using loja_virtual.ViewModel;

namespace loja_virtual.Models
{
  public class User
  {
    public User() { }
    public User(CreateUserViewModel u)
    {
      Login = u.Login;
      Email = u.Email;
      Tipo = u.Tipo;
      Password = u.Password;
    }
    public long Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Tipo { get; set; }
    public string Nome { get; set; }
    public string Password { get; set; }
  }
}