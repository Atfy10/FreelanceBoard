using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(AppDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                await SaveChangesAsync();
            }
            throw new ArgumentNullException(nameof(entity));
            
            
        }
            
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));
            _dbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();

        }


        private async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
