using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //TODOTask<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size);
    }
}
