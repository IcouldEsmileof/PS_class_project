using System.ComponentModel;
using System.Windows;
using CW_less_intuitive_UI.Controller;

namespace CW_less_intuitive_UI.View
{
    public partial class LoginView : INotifyPropertyChanged
    {
        private string _user;
        private string _pass;

        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Pass"));
            }
        }

        public string User
        {
            get => _user;
            set
            {
                _user = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            }
        }

        private readonly LoginController _controller;

        public LoginView(LoginController contr)
        {
            _controller = contr;
            DataContext = this;
            InitializeComponent();
        }

        public void TellUser(string message)
        {
            MessageBox.Show(message);
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            _controller.Login(User, Pass);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}