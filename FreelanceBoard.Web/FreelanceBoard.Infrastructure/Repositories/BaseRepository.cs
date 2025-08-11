using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(AppDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

		public async Task DeleteAsync(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
