using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CW.Controller;

namespace CW.View
{
    public partial class UserRoleView : INotifyPropertyChanged
    {
        private readonly UserController _controller;

        private string _input1="";

        public string InputUser
        {
            get => _input1;
            set
            {
                _input1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputUser"));
            }
        }

        public ObservableCollection<string> Roles { get; set; }

        public UserRoleView(UserController contr)
        {
            _controller = contr;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Submit(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = false;
            string role = Chosen.SelectedIndex.ToString();
            _controller.ChangeUserRole(InputUser, role);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _controller.SecondaryWindowIsClosing();
        }
    }
}