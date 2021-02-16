using Domain.Common;

namespace Domain.Entities
{
    public class Budget : BaseEntity
    {
        public int BudgetTypeId { get; set; }
        public int BudgetVersionId { get; set; }
        public int BudgetYearId { get; set; }
        public int BudgetPeriodId { get; set; }
        public int GlAccountId { get; set; }
        public double Value { get; set; }

        // Navigation Properties
        public BudgetType BudgetType { get; set; }
        public BudgetVersion BudgetVersion { get; set; }
        public BudgetYear BudgetYear { get; set; }
        public BudgetPeriod BudgetPeriod { get; set; }
        public GlAccount GlAccount { get; set; }
    }
}
