using System;
using Avalonia.Collections;
using Avalonia.Media;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;
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
            var newTask = new TodoItem { Name = taskName };
            database.AddItem(taskName);
            TodoItems.Add(new TodoItem { Name = taskName });
            SaveTaskToFile(newTask);
        }
        
        private void SaveTaskToFile(TodoItem task)
        {
            try
            {
                string filePath = "../../../Database/data.txt"; 
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{task.Name} ({task.IsChecked})");
                    Console.WriteLine("Task added to data.txt");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving task to file: {ex.Message}");
            }
        }
    }
}