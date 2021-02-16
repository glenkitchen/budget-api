using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetYear: BaseNameEntity
    {
        public ICollection<BudgetVersion> BudgetVersions { get; set; }
    }
}
