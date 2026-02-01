using System;
using System.Linq;

using var db = new AppDbContext();
db.Database.EnsureCreated();

// Create
db.Todos.Add(new Todo { Title = "学習: EF Core サンプル" });
db.SaveChanges();

// Read
var items = db.Todos.ToList();
foreach (var t in items)
    Console.WriteLine($"{t.Id}: {t.Title}");
