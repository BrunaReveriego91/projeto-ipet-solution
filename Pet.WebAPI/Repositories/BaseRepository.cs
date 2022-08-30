using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Pet.WebAPI.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _context;

        public BaseRepository(TContext context) : base()
        {
            _context = context;
        }

        protected TContext DataContext => _context;

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var result = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual TEntity? Get(int id)
        {
            return _context.Find<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression)
        {
            if (expression is null)
            {
                return _context.Set<TEntity>().AsEnumerable();
            }
            return _context.Set<TEntity>().Where(expression).AsEnumerable();
        }

        public virtual TEntity? Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        TEntity? Get(int id);
        TEntity? Single(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
