using Avalonia.Collections;
using Avalonia.Media;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using Todo.Models;
using UkolnicekZaverecnyProjekt.Database;

namespace UkolnicekZaverecnyProjekt.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<TodoItem> _todoItems;

        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => this.RaiseAndSetIfChanged(ref _todoItems, value);
        }

        public ReactiveCommand<string, Unit> AddTaskCommand { get; }

        public MainWindowViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            AddTaskCommand = ReactiveCommand.Create<string>(AddTask);
        }

        private void AddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                return;

            var database = new Database.Database();
            database.AddItem(taskName);
            TodoItems.Add(new TodoItem { Name = taskName });
        }
    }
}