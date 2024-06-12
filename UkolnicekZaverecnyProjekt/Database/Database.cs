﻿using System;
using System.Collections.Generic;
using System.Text;
using Todo.Models;
 
namespace UkolnicekZaverecnyProjekt.Database;

public class Database
{
    public IEnumerable<TodoItem> GetItems() => new[]
    {
        new TodoItem { Name = "Walk the dog" },
        new TodoItem { Name = "Buy some milk" },
        new TodoItem { Name = "Learn Avalonia", IsChecked = true },
    };
}