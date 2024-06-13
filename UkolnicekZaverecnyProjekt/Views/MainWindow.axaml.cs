using Avalonia.Controls;
using UkolnicekZaverecnyProjekt.ViewModels;

namespace UkolnicekZaverecnyProjekt.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); 
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                var database = new Database.Database();
                foreach (var item in database.GetItems())
                {
                    viewModel.TodoItems.Add(item);
                }
            }
        }
    }
}