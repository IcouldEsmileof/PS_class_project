﻿﻿using CW_less_intuitive_UI.Controller;
  using CW_less_intuitive_UI.Model.UserLoginStuf;

  namespace CW_less_intuitive_UI.Model
{
    public class LoginModel
    {
        private readonly LoginController _controller;

        public LoginModel(LoginController controller)
        {
            this._controller = controller;
        }

        public User GetUser(string username, string password)
        {
            LoginValidation lv = new LoginValidation(username, password, _controller.TellUser);
            return lv.ValidateUserInput(out var user) ? user : null;
        }
    }
}