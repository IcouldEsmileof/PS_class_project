using Interfaces;
using UserLogin;

namespace CW_less_intuitive_UI.Model
{
    public class LoginModel
    {
        private readonly ILoginController _controller;

        public LoginModel(ILoginController controller)
        {
            _controller = controller;
        }

        public User GetUser(string username, string password)
        {
            LoginValidation lv = new LoginValidation(username, password, _controller.TellUser);
            return lv.ValidateUserInput(out var user) ? user : null;
        }
    }
}