using shopDAL.Entities;
using shopDAL.Repositories;
using shopDTO;
using System.Linq;
using System;

namespace shopConsole.Commands
{
    public class ShowProductsCommand : ICommand
    {
        private readonly IProductRepository _repository;
        public string Description => "Товари: Показати всі товари";

        public ShowProductsCommand(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.WriteLine("\nТовари: Список товарів: ");

            Console.WriteLine($"| {"ID",-4} | {"Назва",-28} | {"Кат.",-4} | {"К-сть",5} | {"Ціна",10} | {"Створено",-10} | {"Оновлено",-10} |");
            Console.WriteLine(new string('-', 90));

            var dtos = _repository.getAll().Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId,
                BrandId = p.BrandId,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            foreach (var dto in dtos)
            {
                string createdStr = dto.CreatedAt.HasValue ? dto.CreatedAt.Value.ToString("dd.MM.yyyy") : "---";
                string updatedStr = dto.UpdatedAt.HasValue ? dto.UpdatedAt.Value.ToString("dd.MM.yyyy") : "---";

                string shortName = dto.ProductName;
                if (shortName.Length > 28)
                {
                    shortName = shortName.Substring(0, 25) + "...";
                }

                Console.WriteLine($"| {dto.ProductId,-4} | {shortName,-28} | {dto.CategoryId,-4} | {dto.QuantityInStock,5} | {dto.Price,10:F2} | {createdStr,-10} | {updatedStr,-10} |");
            }
            Console.WriteLine(new string('-', 90) + "\n");
        }
    }


    public class AddProductCommand : ICommand
    {
        private readonly IProductRepository _repository;
        public string Description => "Товари: Додати новий товар";

        public AddProductCommand(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.WriteLine("\nДодавання нового товару");

            Console.Write("Введіть назву товару: ");
            string name = Console.ReadLine() ?? "Без назви";

            Console.Write("Введіть ціну: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            Console.Write("Введіть кількість на складі: ");
            int.TryParse(Console.ReadLine(), out int quantity);

            Console.Write("Введіть ID категорії (від 1 до 20): ");
            int.TryParse(Console.ReadLine(), out int categoryId);

            Console.Write("Введіть ID постачальника (від 1 до 20): ");
            int.TryParse(Console.ReadLine(), out int supplierId);

            Console.Write("Введіть ID бренду (від 1 до 20): ");
            int.TryParse(Console.ReadLine(), out int brandId);

            var dto = new ProductDTO
            {
                ProductName = name,
                Price = price,
                QuantityInStock = quantity,
                CategoryId = categoryId,
                SupplierId = supplierId,
                BrandId = brandId

            };

            _repository.add(new TblProduct
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                QuantityInStock = dto.QuantityInStock,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId,
                BrandId = dto.BrandId,
                CreatedAt = DateTime.Now 
            });

            Console.WriteLine($"\n Товар '{dto.ProductName}' додано до бази!\n");
        }
    }


    public class UpdateProductCommand : ICommand
    {
        private readonly IProductRepository _repository;
        public string Description => "Товари: Оновити ціну товару";

        public UpdateProductCommand(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.Write("\nВведіть ID товару для оновлення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _repository.getById(id);
                if (product != null)
                {
                    var dto = new ProductDTO
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price
                    };

                    Console.WriteLine($"Поточна ціна товару '{dto.ProductName}': {dto.Price}");
                    Console.Write("Введіть нову ціну: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                    {
                        dto.Price = newPrice;
                        product.Price = dto.Price;
                        product.UpdatedAt = DateTime.Now;

                        _repository.update(product);
                        Console.WriteLine("\nЦіну успішно оновлено!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nпомилка! Некоректний формат ціни.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\nпомилка! Товар з ID {id} не знайдено.\n");
                }
            }
        }
    }


    public class DeleteProductCommand : ICommand
    {
        private readonly IProductRepository _repository;
        public string Description => "Товари: Видалити товар за ID";

        public DeleteProductCommand(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.Write("\nВведіть ID товару для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var entity = _repository.getById(id);
                if (entity != null)
                {
                    var dto = new ProductDTO
                    {
                        ProductId = entity.ProductId,
                        ProductName = entity.ProductName
                    };

                    _repository.delete(dto.ProductId);
                    Console.WriteLine($"\nТовар '{dto.ProductName}' (ID: {dto.ProductId}) видалено!\n");
                }
                else
                {
                    Console.WriteLine($"\nпомилка! Товар з ID {id} не знайдено.\n");
                }
            }
        }
    }
}