using Microsoft.EntityFrameworkCore;
using RefactorChallenge.Application.Contracts;
using RefactoringChallenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RefactorChallenge.Persistence.Repositories
{
    public class GenericRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly NorthwindDbContext _dbContext;

        public GenericRepository(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);         

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<ICollection<T>> Find(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().AsQueryable().Where(predicate).ToListAsync();
       
        public virtual async Task<IReadOnlyList<T>> ListAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> QueryableList() => _dbContext.Set<T>().AsQueryable();

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();            
        }

        public virtual async Task DeleteAllAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
