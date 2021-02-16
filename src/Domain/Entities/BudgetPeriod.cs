
using Domain.Common;
using System;

namespace Domain.Entities
{
    public class BudgetPeriod: BaseNameEntity
    {
        public int BudgetYearId { get; set; }
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Navigation Properties
        public BudgetYear BudgetYear { get; set; }
    }
}
