using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetType : BaseOption { }
    public class BudgetVersion : BaseOption {
        public ICollection<BudgetYear> BudgetYears { get; set; }
    }
    public class GlAccountType : BaseOption { }
    public class SalesRegion : BaseOption { }
}
