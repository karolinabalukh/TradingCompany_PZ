using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using shopDAL.Entities;
using shopDAL.Repositories;
using System.Linq;

namespace shopTests
{
    [TestFixture]
    public class SupplierRepositoryTests
    {
        private TradingDbContext _context;
        private SupplierRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTradingDB_Sup_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new TradingDbContext(options);
            _context.TblSuppliers.Add(new TblSupplier { 
                SupplierId = 1, 
                CompanyName = "TechDistribution LLC" });
            _context.TblSuppliers.Add(new TblSupplier { 
                SupplierId = 2, 
                CompanyName = "Global Electronics" });
            _context.SaveChanges();
            _repository = new SupplierRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsAllSuppliers()
        {
            var result = _repository.getAll().ToList();
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Add_AddsNewSupplier()
        {
            var newSup = new TblSupplier { 
                SupplierId = 3, 
                CompanyName = "MegaTrade Ukraine" };
            _repository.add(newSup);
            Assert.That(_context.TblSuppliers.ToList(), Has.Count.EqualTo(3));
        }

        [Test]
        public void Delete_RemovesSupplier()
        {
            _repository.delete(1);
            Assert.That(_context.TblSuppliers.ToList(), Has.Count.EqualTo(1));
        }
    }
}