using Avalonia.Controls;
using UkolnicekZaverecnyProjekt.Viewmodels;
using UkolnicekZaverecnyProjekt.ViewModels;

namespace UkolnicekZaverecnyProjekt.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Nastavení ViewModelu jako DataContext
            InitializeTasks(); // Inicializace úkolů při spuštění okna
        }

        // Metoda pro inicializaci úkolů z databáze
        private void InitializeTasks()
        {
            var viewModel = DataContext as MainWindowViewModel; // Získání ViewModelu z DataContextu
            if (viewModel != null)
            {
                var database = new Database.Database(); // Vytvoření nové instance databáze
                foreach (var item in database.GetItems()) // Procházení úkolů z databáze
                {
                    viewModel.TodoItems.Add(item); // Přidání úkolů do kolekce ve ViewModelu
                }
            }
        }
    }
}