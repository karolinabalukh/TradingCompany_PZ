using shopDAL.Repositories;
using shopDTO;
using System.Collections.Generic;
using System.Linq;

namespace shopBL
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.getAll();

            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock
            }).ToList();
        }
    }
}