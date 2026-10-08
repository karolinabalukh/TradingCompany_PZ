using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;

namespace shopDAL.Repositories
{
    public class BrandRepository : BaseRepository<TblBrand>, IBrandRepository
    {
        public BrandRepository(TradingDbContext context) : base(context)
        {
        }
    }
}