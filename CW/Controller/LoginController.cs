using CW.Model;
using CW.View;

namespace CW.Controller
{
    public class LoginController
    {
        private readonly LoginView _view;
        private readonly LoginModel _model;

        public LoginController()
        {
            _model = new LoginModel(this);

            _view = new LoginView(this);
            _view.Show();
        }

        public void Login(string username, string password)
        {
            User user = _model.GetUser(username, password);
            if (user != null)
            {
                new UserController(user);
                _view.Close();
            }
        }

        public void TellUser(string message)
        {
            _view.TellUser(message);
        }
    }
}