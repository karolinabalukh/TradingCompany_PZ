using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;

namespace shopDAL.Repositories
{
    public class SupplierRepository : BaseRepository<TblSupplier>, ISupplierRepository
    {
        public SupplierRepository(TradingDbContext context) : base(context)
        {
        }
    }
}