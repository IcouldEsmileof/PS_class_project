using System.Windows;
using CW_less_intuitive_UI.Model;
using CW_less_intuitive_UI.View;
using Interfaces;
using UserLogin;

namespace CW_less_intuitive_UI.Controller
{
    public class UserController : IUserController
    {
        private readonly User _user;
        private readonly UserView _view;
        private UserActiveToView _userActiveToView;
        private UserRoleView _userRoleView;
        private readonly UserModel _model;

        public UserController(User user)
        {
            _user = user;

            _model = new UserModel(this);
            _view = new UserView(this);
            _view.Show();
            ActivateMainMenu();
        }

        public void ShowUser(string output)
        {
            _view.Output = output;
        }

        public void AppendToShowUser(string output)
        {
            ShowUser(_view.Output + output);
        }

        public void ActivateMainMenu()
        {
            _view.BtnSubmit.IsEnabled = _model.ShowMainMenu(_user);
        }

        public void ChangeUserRole()
        {
            _userRoleView = new UserRoleView(this);
            _userRoleView.Show();
            _userRoleView.Output = _model.GetRoleMenu();
        }

        public void ChangeUserRole(string username, string role)
        {
            if (_model.ChangeRole(username, role))
            {
                _userRoleView.Output = "Success";
                _view.BtnSubmit.IsEnabled = true;
            }
            else
            {
                _userRoleView.Output = _model.GetRoleMenu();
                _userRoleView.BtnSubmit.IsEnabled = true;
            }
        }

        public void ChangeUserActiveTo()
        {
            _userActiveToView = new UserActiveToView(this);
            _userActiveToView.Show();
            _userActiveToView.Output = _model.GetActiveToMenu();
        }

        public void ChangeUserActiveTo(string username, string activeTo)
        {
            if (_model.ChangeActiveTo(username, activeTo))
            {
                _userActiveToView.Output = "Success";
                _view.BtnSubmit.IsEnabled = true;
            }
            else
            {
                _userActiveToView.Output = _model.GetActiveToMenu();
                _userActiveToView.BtnSubmit.IsEnabled = true;
            }
        }

        public void AlertUser(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowUsersList()
        {
            _model.ShowUserList();
        }

        public void ShowLogs()
        {
            _model.ShowLogs();
        }

        public void ShowCurrentActivity()
        {
            _model.ShowCurrentActivity();
        }

        public void ProcessMainInput(string input)
        {
            IUserController.ToExec toExec = _model.ProcessMainInput(input);
            toExec?.Invoke();
        }

        public void Close()
        {
            _view.ToClose = true;
        }
    }
}