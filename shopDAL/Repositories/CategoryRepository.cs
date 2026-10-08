using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;

namespace shopDAL.Repositories
{
    public class CategoryRepository : BaseRepository<TblCategory>, ICategoryRepository
    {
        public CategoryRepository(TradingDbContext context) : base(context)
        {
        }
    }
}