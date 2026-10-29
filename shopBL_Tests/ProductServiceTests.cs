using Moq;
using shopBL;
using shopDAL.Entities;
using shopDAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace shopBL_Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public void GetAllProducts_ReturnsMappedDTOs()
        {
            var dbProducts = new List<TblProduct>
            {
                new TblProduct { ProductId = 1, ProductName = "Ноутбук", Price = 45000, QuantityInStock = 10 },
                new TblProduct { ProductId = 2, ProductName = "Мишка", Price = 1500, QuantityInStock = 50 }
            };

            _mockRepo.Setup(r => r.getAll()).Returns(dbProducts);
            var result = _productService.GetAllProducts().ToList();
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].ProductId);
            Assert.Equal("Ноутбук", result[0].ProductName);
            Assert.Equal(45000, result[0].Price);

            Assert.Equal("Мишка", result[1].ProductName);
        }

        [Fact]
        public void GetAllProducts_EmptyRepository_ReturnsEmptyList()
        {
            _mockRepo.Setup(r => r.getAll()).Returns(new List<TblProduct>());
            var result = _productService.GetAllProducts();
            Assert.Empty(result);
        }
    }
}