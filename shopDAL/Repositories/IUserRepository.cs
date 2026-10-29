using System;
using System.Collections.Generic;
using System.Text;
using shopDAL.Entities;
using shopDAL_EF;

namespace shopDAL.Repositories
{
    public interface IUserRepository : IRepository<TblUser>
    {
        TblUser? GetByLogin(string login);
        void CreateUserWithCard(TblUser user, string cardNumber);
    }
}