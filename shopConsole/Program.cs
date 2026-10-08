using Microsoft.EntityFrameworkCore;
using shopConsole.Commands;
using shopDAL.Entities;
using shopDAL.Repositories;
using System;
using System.Collections.Generic;

string connectionString = System.IO.File.ReadAllText("db_connection.txt").Trim();
var options = new DbContextOptionsBuilder<TradingDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new TradingDbContext(options);
IProductRepository productRepo = new ProductRepository(context);
ICategoryRepository categoryRepo = new CategoryRepository(context);

var commands = new List<ICommand>
{
    new ShowProductsCommand(productRepo),
    new AddProductCommand(productRepo),
    new UpdateProductCommand(productRepo),
    new DeleteProductCommand(productRepo),
    
    new ShowCategoriesCommand(categoryRepo),
    new AddCategoryCommand(categoryRepo),
    new UpdateCategoryCommand(categoryRepo),
    new DeleteCategoryCommand(categoryRepo)
};

Console.OutputEncoding = System.Text.Encoding.UTF8;
bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\nTrading Company");
    for (int i = 0; i < commands.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {commands[i].Description}");
    }
    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть дію: ");

    string? input = Console.ReadLine();

    if (input == "0")
    {
        isRunning = false;
        continue;
    }

    if (int.TryParse(input, out int choice) && choice > 0 && choice <= commands.Count)
    {
        commands[choice - 1].Execute();
    }
    else
    {
        Console.WriteLine("Невірний вибір. Спробуйте ще раз.\n");
    }
}