using System.Windows;
using CW.Controller;

namespace CW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            new LoginController();
            Close();
            InitializeComponent();
        }
    }
}