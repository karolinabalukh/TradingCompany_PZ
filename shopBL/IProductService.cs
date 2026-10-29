using shopDTO;
using System.Collections.Generic;

namespace shopBL
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
    }
}