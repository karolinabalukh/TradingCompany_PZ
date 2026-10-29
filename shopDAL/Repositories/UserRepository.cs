using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;
using shopDAL_EF;
using System.Linq;

namespace shopDAL.Repositories
{
    public class UserRepository : BaseRepository<TblUser>, IUserRepository
    {
        public UserRepository(TradingDbContext context) : base(context)
        {
        }


        public TblUser? GetByLogin(string login)
        {
            return _dbSet.FirstOrDefault(
                u => u.Login == login
            );
        }



        public void CreateUserWithCard(TblUser user, string cardNumber)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _dbSet.Add(user);
                _context.SaveChanges();

                var card = new TblBankCard
                {
                    UserId = user.UserId,
                    Number = cardNumber
                };

                _context.TblBankCards.Add(card);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}