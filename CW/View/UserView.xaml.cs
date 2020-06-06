using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CW.Controller;

namespace CW.View
{
    public partial class UserView : INotifyPropertyChanged
    {
        private readonly UserController _controller;

        private string _output;


        public event PropertyChangedEventHandler PropertyChanged;

        public string Output
        {
            get => _output;
            set
            {
                _output = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Output"));
            }
        }

        public UserView(UserController contr)
        {
            _controller = contr;
            InitializeComponent();
        }

        private void ChangeRole(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _controller.ChangeUserRole();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            EnableButtons();
            _controller.ActivateMainMenu();
            if (_controller.isUserAdmin)
            {
                _controller.AppendToShowUser("Каква е следващата Ви заявка?\n");
            }
        }

        private void ChangeActivity(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _controller.ChangeUserActiveTo();
        }

        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _controller.ShowUsersList();
        }

        private void ShowLogs(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _controller.ShowLogs();
        }

        private void ShowCurrentLogs(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _controller.ShowCurrentActivity();
        }

        public void DisableButtons()
        {
            BtnReset.IsEnabled = true;
            BtnChangeActivity.IsEnabled = false;
            BtnChangeRole.IsEnabled = false;
            BtnShowLogs.IsEnabled = false;
            BtnShowUsers.IsEnabled = false;
            BtnShowCurrentLogs.IsEnabled = false;
        }

        public void EnableButtons()
        {
            BtnReset.IsEnabled = false;
            BtnChangeActivity.IsEnabled = true;
            BtnChangeRole.IsEnabled = true;
            BtnShowLogs.IsEnabled = true;
            BtnShowUsers.IsEnabled = true;
            BtnShowCurrentLogs.IsEnabled = true;
        }
    }
}