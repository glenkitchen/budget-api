using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetYear: NameEntity
    {
        // Navigation Properties
        public ICollection<BudgetVersion> BudgetVersions { get; set; }
    }
}
