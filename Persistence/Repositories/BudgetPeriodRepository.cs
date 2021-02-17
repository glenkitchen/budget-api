using Application.Interfaces.Persistence;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class BudgetPeriodRepository : BaseRepository<BudgetPeriod>, IBudgetPeriodRepository
    {
        public BudgetPeriodRepository(BudgetDbContext dbContext) : base(dbContext)
        {
        }
    }
}
