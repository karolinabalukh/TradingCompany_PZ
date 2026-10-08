using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;

namespace shopDAL.Repositories
{
    public class ProductRepository : BaseRepository<TblProduct>, IProductRepository
    {
        public ProductRepository(TradingDbContext context) : base(context)
        {
        }
    }
}