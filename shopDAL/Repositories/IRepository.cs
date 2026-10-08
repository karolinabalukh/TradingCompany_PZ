using System;
using System.Collections.Generic;
using System.Text;

namespace shopDAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> getAll();
        TEntity? getById(int id);
        void add(TEntity entity);
        void update(TEntity entity);
        void delete(int id);
    }
}
