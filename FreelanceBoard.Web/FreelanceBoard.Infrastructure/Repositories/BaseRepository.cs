﻿using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Repositories
{
    internal class BaseRepository<TEntity>(AppDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext = dbContext 
            ?? throw new ArgumentNullException(nameof(dbContext));

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
