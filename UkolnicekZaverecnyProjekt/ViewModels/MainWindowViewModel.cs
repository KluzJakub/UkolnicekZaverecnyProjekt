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
        }

        // Metoda pro přidání nového úkolu do kolekce, databáze a souboru
        private void AddTask(string taskName)
        {
            // Kontrola, zda je název úkolu platný
            if (string.IsNullOrWhiteSpace(taskName))
                return;

            // Vytvoření nové instance databáze
            var database = new Database.Database();

            // Vytvoření nového úkolu s poskytnutým názvem
            var newTask = new TodoItem { Name = taskName };

            // Přidání nového úkolu do databáze
            database.AddItem(taskName);

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
    }
}