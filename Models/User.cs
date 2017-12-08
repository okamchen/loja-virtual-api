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
      Name = u.Name;
      Profile = u.Profile == null ? "USER" : u.Profile;
      Password = u.Password;
    }
    public long Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
  }
}