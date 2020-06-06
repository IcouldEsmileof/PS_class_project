using System.ComponentModel;
using System.Windows;
using CW.Controller;

namespace CW.View
{
    public partial class UserRoleView : INotifyPropertyChanged
    {
        private readonly UserController _controller;

        private string _input1;
        private string _input2;
        private string _output;

        public string InputUser
        {
            get => _input1;
            set
            {
                _input1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputUser"));
            }
        }

        public string InputRole
        {
            get => _input2;
            set
            {
                _input2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputRole"));
            }
        }

        public string Output
        {
            get => _output;
            set
            {
                _output = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Output"));
            }
        }

        public UserRoleView(UserController contr)
        {
            _controller = contr;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Submit(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = false;
            //_controller.AlertUser(InputUser + InputRole);
            _controller.ChangeUserRole(InputUser, InputRole);
        }
    }
}