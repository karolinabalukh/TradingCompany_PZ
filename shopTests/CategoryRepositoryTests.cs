using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using shopDAL.Entities;
using shopDAL.Repositories;
using System.Linq;

namespace shopTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private TradingDbContext _context;
        private CategoryRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTradingDB_Cat_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TradingDbContext(options);
            _context.TblCategories.Add(new TblCategory { 
                CategoryId = 1, 
                CategoryName = "Електроніка" });
            _context.TblCategories.Add(new TblCategory { 
                CategoryId = 2, 
                CategoryName = "Побутова техніка" });
            _context.SaveChanges();
            _repository = new CategoryRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsAllCategories()
        {
            var result = _repository.getAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Add_AddsNewCategory()
        {
            var newCat = new TblCategory { 
                CategoryId = 3, 
                CategoryName = "Меблі" };
            _repository.add(newCat);
            Assert.That(_context.TblCategories.ToList(), Has.Count.EqualTo(3));
        }

        [Test]
        public void Delete_RemovesCategory()
        {
            _repository.delete(1);
            Assert.That(_context.TblCategories.ToList(), Has.Count.EqualTo(1));
        }
    }
}