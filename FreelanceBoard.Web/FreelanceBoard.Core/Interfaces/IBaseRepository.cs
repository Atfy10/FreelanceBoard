using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> UserExistsAsync(string userId);
        Task<TEntity?> GetByIdsAsync(params object[] id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(string id);
		Task DeleteAsync(TEntity entity);

	}
}
