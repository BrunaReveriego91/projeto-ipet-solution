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

        public virtual async Task Add(TEntity entity)
        {
            await _context.AddAsync(entity);
            await Commit();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public virtual TEntity? Get(int id)
        {
            return _context.Find<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression is null)
            {
                return _context.Set<TEntity>().AsEnumerable();
            }
            else
            {

                return _context.Set<TEntity>().Where(expression).AsEnumerable();
            }
        }

        public virtual TEntity? Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }
    }

    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        TEntity? Get(int id);
        TEntity? Single(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null);
        Task Commit();
    }
}
