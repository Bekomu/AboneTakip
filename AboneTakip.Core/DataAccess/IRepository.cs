using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Core.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<int> Save();
    }
}
