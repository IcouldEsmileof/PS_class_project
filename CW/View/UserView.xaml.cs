using System.ComponentModel;
using System.Windows;
using CW.Controller;

namespace CW.View
{
    public partial class UserView : INotifyPropertyChanged
    {
        private readonly UserController _controller;

        private string _input;
        private string _output;

        public bool ToClose = false;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input"));
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

        public UserView(UserController contr)
        {
            _controller = contr;
            InitializeComponent();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void SubmitFromMainMenu(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = false;
            _controller.ProcessMainInput(Input);
            if (ToClose)
            {
                Close();
            }

            BtnReset.IsEnabled = true;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            BtnReset.IsEnabled = false;
            BtnSubmit.IsEnabled = true;
            _controller.ActivateMainMenu();
        }
    }
}