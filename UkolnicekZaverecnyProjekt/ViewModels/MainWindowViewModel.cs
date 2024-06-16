using System;
using Avalonia.Collections;
using Avalonia.Media;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using Todo.Models;
using UkolnicekZaverecnyProjekt.Database;
using UkolnicekZaverecnyProjekt.ViewModels;

namespace UkolnicekZaverecnyProjekt.Viewmodels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // ObservableCollection pro ukládání úkolů
        private ObservableCollection<TodoItem> _todoItems;

        // Veřejná vlastnost pro datovou vazbu s View
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => this.RaiseAndSetIfChanged(ref _todoItems, value);
        }

        // Příkaz pro přidání nového úkolu
        public ReactiveCommand<string, Unit> AddTaskCommand { get; }

        // Konstruktor inicializuje kolekci a příkaz
        public MainWindowViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            AddTaskCommand = ReactiveCommand.Create<string>(AddTask);

            // Načtení úkolů ze souboru při inicializaci ViewModelu
            LoadTasksFromFile();
        }

        // Metoda pro přidání nového úkolu do kolekce, databáze a souboru
        private void AddTask(string taskName)
        {
            // Kontrola, zda je název úkolu platný
            if (string.IsNullOrWhiteSpace(taskName))
                return;

            // Vytvoření nového úkolu s poskytnutým názvem
            var newTask = new TodoItem { Name = taskName };

            // Přidání nového úkolu do ObservableCollection
            TodoItems.Add(newTask);

            // Uložení nového úkolu do souboru
            SaveTaskToFile(newTask);
        }

        // Metoda pro uložení úkolu do textového souboru
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

        // Metoda pro načtení úkolů ze souboru
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání úkolů ze souboru: {ex.Message}");
            }
        }
    }
}
