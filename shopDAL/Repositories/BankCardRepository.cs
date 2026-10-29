using shopDAL.Entities;
using shopDAL_EF;
using shopDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopDAL.Repositories
{
    public class BankCardRepository : BaseRepository<TblBankCard>, IBankCardRepository
    {
        public BankCardRepository(TradingDbContext context) : base(context)
        {
        }
    }
}