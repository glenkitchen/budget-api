using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetType : OptionEntity { }
    public class BudgetVersion : OptionEntity {
        // Navigation Properties
        public ICollection<BudgetYear> BudgetYears { get; set; }
    }
    public class GlAccountType : OptionEntity { }
    public class SalesRegion : OptionEntity { }
}
