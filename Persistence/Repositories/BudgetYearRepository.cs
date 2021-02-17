using Application.Interfaces.Persistence;
using Domain.Entities;

namespace Persistence.Repositories
{
    class BudgetYearRepository : BaseRepository<BudgetYear>, IBudgetYearRepository
    {
        public BudgetYearRepository(BudgetDbContext dbContext) : base(dbContext)
        {
        }
    }
}
