using shopDAL.Entities;
using shopDAL.Repositories;
using shopDTO;

using System.Linq;
using System;

namespace shopConsole.Commands
{
    public class ShowCategoriesCommand : ICommand
    {
        private readonly ICategoryRepository _repository;
        public string Description => "Категорії: Показати всі";

        public ShowCategoriesCommand(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.WriteLine("\nСписок категорій");
            Console.WriteLine($"| {"ID",-4} | {"Назва категорії",-30} |");

            var dtos = _repository.getAll().Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).ToList();

            foreach (var dto in dtos)
            {
                Console.WriteLine($"| {dto.CategoryId,-4} | {dto.CategoryName,-30} |");
            }
        }
    }


    public class AddCategoryCommand : ICommand
    {
        private readonly ICategoryRepository _repository;
        public string Description => "Категорії: Додати нову";

        public AddCategoryCommand(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.Write("\nВведіть назву нової категорії: ");
            string name = Console.ReadLine() ?? "Без назви";

            var dto = new CategoryDTO { CategoryName = name };
            _repository.add(new TblCategory { CategoryName = dto.CategoryName });

            Console.WriteLine($"\nКатегорію '{dto.CategoryName}' додано!\n");
        }
    }


    public class UpdateCategoryCommand : ICommand
    {
        private readonly ICategoryRepository _repository;
        public string Description => "Категорії: Змінити назву";

        public UpdateCategoryCommand(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.Write("\nВведіть ID категорії для оновлення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var category = _repository.getById(id);
                if (category != null)
                {
                    var dto = new CategoryDTO
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    };

                    Console.WriteLine($"Поточна назва: {dto.CategoryName}");
                    Console.Write("Введіть нову назву: ");
                    string newName = Console.ReadLine() ?? dto.CategoryName;

                    dto.CategoryName = newName; 
                    category.CategoryName = dto.CategoryName; 

                    _repository.update(category);
                    Console.WriteLine("\nКатегорію оновлено!\n");
                }
                else
                {
                    Console.WriteLine($"\nпомилка! Категорію з ID {id} не знайдено.\n");
                }
            }
        }
    }


    public class DeleteCategoryCommand : ICommand
    {
        private readonly ICategoryRepository _repository;
        public string Description => "Категорії: Видалити";

        public DeleteCategoryCommand(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.Write("\nВведіть ID категорії для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var category = _repository.getById(id);
                if (category != null)
                {
                    var dto = new CategoryDTO
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    };

                    try
                    {
                        _repository.delete(dto.CategoryId);
                        Console.WriteLine($"\n Категорію '{dto.CategoryName}' видалено!\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"\nпомилка! Неможливо видалити категорію, до неї прив'язані товари.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\nпомилка! Категорію з ID {id} не знайдено.\n");
                }
            }
        }
    }
}