using Avalonia.Controls;
using UkolnicekZaverecnyProjekt.Viewmodels;

namespace UkolnicekZaverecnyProjekt.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Nastavení ViewModelu jako DataContext
        }
    }
}