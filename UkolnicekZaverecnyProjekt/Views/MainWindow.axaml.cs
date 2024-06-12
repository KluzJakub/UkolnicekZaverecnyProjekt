using System.Net.Mime;
using Avalonia.Controls;

namespace UkolnicekZaverecnyProjekt.Views;

public partial class MainWindow : Window
{
        public MainWindow()
        {
            InitializeComponent();
            tasks.ItemsSource = new string[]
                {"První úkol", "Druhý úkol", "Třetí úkol" };
        }
}