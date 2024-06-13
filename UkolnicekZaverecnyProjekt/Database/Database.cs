using System.Collections.Generic;
using Todo.Models;

namespace UkolnicekZaverecnyProjekt.Database
{
    public class Database
    {
        private List<TodoItem> _items = new List<TodoItem>
        {
            new TodoItem { Name = "Task 1" },
            new TodoItem { Name = "Task 2" },
            new TodoItem { Name = "Task 3", IsChecked = true }
        };

        public IEnumerable<TodoItem> GetItems()
        {
            return _items;
        }

        public void AddItem(string name)
        {
            _items.Add(new TodoItem { Name = name });
        }
    }
}