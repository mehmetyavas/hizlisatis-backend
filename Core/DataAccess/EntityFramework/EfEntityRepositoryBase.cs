using Core.Abstract;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()

    {
        protected readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
           => _context = context;

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _context.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
            => _context.Set<TEntity>().SingleOrDefault(filter)!;


        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
           return filter == null
             ? _context.Set<TEntity>().ToList()
             : _context.Set<TEntity>().Where(filter).ToList();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
            => _context.Set<TEntity>().SingleOrDefaultAsync(expression)!;

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
            => expression == null
             ? await _context.Set<TEntity>().CountAsync()
             : await _context.Set<TEntity>().CountAsync(expression);

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
            => expression == null
             ? await _context.Set<TEntity>().AsNoTracking().ToListAsync()
             : await _context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();

        public void Update(TEntity entity)
        {
            _context.Update(entity);
             _context.SaveChanges();

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
