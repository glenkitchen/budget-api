using Application.Exceptions;
using Application.Interfaces.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BudgetDbContext _dbContext;

        public BaseRepository(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                                   .AsNoTracking()
                                   .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetEntityAsync(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);               
            }

            return await _dbContext.Set<TEntity>()
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            // _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            
            //await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            //?
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            //_dbContext.Set<TEntity>().Remove(entity);            
            //?
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }      
    }
}
