using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using shopDAL.Entities;
using shopDAL.Repositories;
using System.Linq;

namespace shopTests
{
    [TestFixture]
    public class BrandRepositoryTests
    {
        private TradingDbContext _context;
        private BrandRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTradingDB_Brand_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TradingDbContext(options);
            _context.TblBrands.Add(new TblBrand { 
                BrandId = 1, 
                BrandName = "Apple" });
            _context.TblBrands.Add(new TblBrand { 
                BrandId = 2, 
                BrandName = "Samsung" });
            _context.SaveChanges();

            _repository = new BrandRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsAllBrands()
        {
            var result = _repository.getAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Add_AddsNewBrand()
        {
            var newBrand = new TblBrand { 
                BrandId = 3, 
                BrandName = "Asus" };
            _repository.add(newBrand);
            Assert.That(_context.TblBrands.ToList(), Has.Count.EqualTo(3));
        }

        [Test]
        public void Delete_RemovesBrand()
        {
            _repository.delete(1);
            Assert.That(_context.TblBrands.ToList(), Has.Count.EqualTo(1));
        }
    }
}