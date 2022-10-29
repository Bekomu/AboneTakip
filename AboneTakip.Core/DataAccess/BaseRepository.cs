using AboneTakip.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Core.DataAccess
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : BaseEntity where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _table;

        public BaseRepository(TContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _table.AddAsync(entity);
            return await Save() > 0 ? entity : null;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _table.Remove(entity);
            return await Save() > 0;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await _table.SingleOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _table.Where(x => x.Status != Enums.Status.Deleted).ToListAsync();
        }

        public Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var entity = await _table.FindAsync(id);

            return entity.Status != Enums.Status.Deleted ? entity : null;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            return await Save() > 0 ? entity : null;
        }
    }
}
