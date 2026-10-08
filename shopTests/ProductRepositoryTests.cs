using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using shopDAL.Entities;
using shopDAL.Repositories;
using System.Linq;

namespace shopTests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private TradingDbContext _context;
        private ProductRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTradingDB_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TradingDbContext(options);
            _context.TblProducts.Add(new TblProduct
            {
                ProductId = 1,
                ProductName = "Тестовий Ноутбук",
                Price = 20000,
                QuantityInStock = 5,
                CategoryId = 1,
                BrandId = 1,
                SupplierId = 1
            });
            _context.TblProducts.Add(new TblProduct
            {
                ProductId = 2,
                ProductName = "Тестовий Телефон",
                Price = 15000,
                QuantityInStock = 10,
                CategoryId = 1,
                BrandId = 1,
                SupplierId = 1
            });

            _context.SaveChanges();
            _repository = new ProductRepository(_context);
        }




        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        [Category("Read")]
        [Description("метод getAll повертає всі записи з бази")]
        public void GetAll_ReturnsAllProducts()
        {
            var result = _repository.getAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].ProductName, Is.EqualTo("Тестовий Ноутбук"));
        }

        [Test]
        [Category("Create")]
        public void Add_AddsNewProductToDatabase()
        {
            var newProduct = new TblProduct
            {
                ProductId = 3,
                ProductName = "Новий планшет",
                Price = 12000,
                QuantityInStock = 7,
                CategoryId = 1,
                BrandId = 1,
                SupplierId = 1
            };

            _repository.add(newProduct);
            var allProducts = _context.TblProducts.ToList();
            Assert.Multiple(() =>
            {
                Assert.That(allProducts, Has.Count.EqualTo(3), "Кількість товарів має збільшитись до 3");
                Assert.That(allProducts.Any(p => p.ProductName == "Новий планшет"), Is.True, "Новий товар має бути в базі");
            });
        }

        [Test]
        [Category("Delete")]
        public void Delete_RemovesProductFromDatabase()
        {
            _repository.delete(1);
            var allProducts = _context.TblProducts.ToList();
            var deletedProduct = _context.TblProducts.Find(1);

            Assert.Multiple(() =>
            {
                Assert.That(allProducts, Has.Count.EqualTo(1));
                Assert.That(deletedProduct, Is.Null);
            });
        }
    }
}