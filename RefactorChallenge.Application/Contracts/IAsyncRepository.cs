using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RefactorChallenge.Application.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync(IEnumerable<T> entities);
        Task<ICollection<T>> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> QueryableList();
    }
}
