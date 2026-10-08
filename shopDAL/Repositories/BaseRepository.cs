using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using shopDAL.Entities;
using System.Linq;

namespace shopDAL.Repositories
{
    public abstract class BaseRepository<TEntity> : 
        IRepository<TEntity> 
        where TEntity : class


    {
        protected readonly TradingDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TradingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> getAll() 
            => _dbSet.ToList();

        public virtual TEntity? getById(int id) 
            => _dbSet.Find(id);

        public virtual void add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public virtual void delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}