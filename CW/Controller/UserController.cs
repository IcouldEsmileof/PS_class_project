using System;
using System.Collections.ObjectModel;
using System.Windows;
using CW.View;
using CW.Model;
using Interfaces;
using UserLogin;

namespace CW.Controller
{
    public class UserController : IUserController
    {
        private readonly User _user;
        private readonly UserView _view;
        private UserActiveToView _userActiveToView;
        private UserRoleView _userRoleView;
        private readonly UserModel _model;
        public bool isUserAdmin = false;
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
            if (_model.ShowMainMenu(_user))
            {
                _view.EnableButtons();
                isUserAdmin = true;
            }
            else
            {
                _view.DisableButtons();
                isUserAdmin = false;
                _view.BtnReset.IsEnabled = false;
            }
        }

        public void ChangeUserRole()
        {
            _userRoleView = new UserRoleView(this);
            _userRoleView.Topmost=true;
            _userRoleView.Roles = new ObservableCollection<string>();
            foreach (var role in _model.GetRoles())
            {
                _userRoleView.Roles.Add(role);
            }
            _userRoleView.ShowDialog();
        }

        public void ChangeUserRole(string username, string role)
        {
            if (_model.ChangeRole(username, role))
            {
                AlertUser( "Ролята беше сменена успешно");
            }
            else
            {
                _userRoleView.Roles = new ObservableCollection<string>(_model.GetRoles());
                _userRoleView.BtnSubmit.IsEnabled = true;
            }
        }

        public void ChangeUserActiveTo()
        {
            _userActiveToView = new UserActiveToView(this);
            _userActiveToView.Topmost=true;
            _userActiveToView.Output = _model.GetActiveToMenu();
            _userActiveToView.ShowDialog();
        }

        public void ChangeUserActiveTo(string username, string activeTo)
        {
            if (_model.ChangeActiveTo(username, activeTo))
            {
                AlertUser("Датата на активност беше сменена успешно");
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
            _view.Close();
        }

        public void SecondaryWindowIsClosing()
        {
            if (_user.ActiveTo != null && (_user.Role == (int) UserRole.Admin && _user.ActiveTo.Value.CompareTo(DateTime.Now)>0))
            {
                _view.EnableButtons();
            }
        }
    }
}