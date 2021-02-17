using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class GlAccount: CodeNameEntity
    {
        public int GlAccountTypeId { get; set; }
        public int? SalesRegionId { get; set; }

        // Navigation Properties
        public GlAccountType GlAccountType { get; set; }
        public SalesRegion SalesRegion { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
