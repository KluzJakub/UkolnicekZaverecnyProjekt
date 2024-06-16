using Avalonia.Collections;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Todo.Models;
using UkolnicekZaverecnyProjekt.ViewModels;

namespace UkolnicekZaverecnyProjekt.Viewmodels
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

            LoadTasksFromFile();
        }

        private void AddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                return;

            var newTask = new TodoItem { Name = taskName };
            TodoItems.Add(newTask);

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
                    Console.WriteLine("Úkol přidán do data.txt");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při ukládání úkolu do souboru: {ex.Message}");
            }
        }

        private void LoadTasksFromFile()
        {
            try
            {
                string filePath = "../../../Database/data.txt";
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(new[] { " (" }, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                var name = parts[0].Trim();
                                var isChecked = parts[1].Replace(")", "").Trim().ToLower() == "true";
                                TodoItems.Add(new TodoItem { Name = name, IsChecked = isChecked });
                            }
                        }
                    }
                }

                // Filtrace zobrazení úkolů (neukazuj ty, které jsou IsChecked == true)
                TodoItems = new ObservableCollection<TodoItem>(TodoItems.Where(item => !item.IsChecked));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání úkolů ze souboru: {ex.Message}");
            }
        }
    }
}
