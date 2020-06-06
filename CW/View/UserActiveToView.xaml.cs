using System;
using System.ComponentModel;
using System.Windows;
using CW.Controller;

namespace CW.View
{
    public partial class UserActiveToView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public string InputActiveTo
        {
            get => _input2;
            set
            {
                _input2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputActiveTo"));
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

        public UserActiveToView(UserController contr)
        {
            _controller = contr;
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = false;
            _controller.ChangeUserActiveTo(InputUser, InputActiveTo);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _controller.SecondaryWindowIsClosing();
        }
    }
}