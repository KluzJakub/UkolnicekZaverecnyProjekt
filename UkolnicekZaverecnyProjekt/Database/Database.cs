using System.Collections.Generic;
using Todo.Models;

namespace UkolnicekZaverecnyProjekt.Database
{
    public class Database
    {
        // Soukromý seznam úkolů simulující databázi
        private List<TodoItem> _items = new List<TodoItem>
        {
            new TodoItem { Name = "Task 1" },
            new TodoItem { Name = "Task 2" },
            new TodoItem { Name = "Task 3", IsChecked = true } // Předvyplněný úkol s označením dokončení
        };

        // Metoda pro získání všech úkolů z databáze
        public IEnumerable<TodoItem> GetItems()
        {
            return _items;
        }

        // Metoda pro přidání nového úkolu do databáze
        public void AddItem(string name)
        {
            _items.Add(new TodoItem { Name = name });
        }
    }
}