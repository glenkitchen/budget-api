using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetPeriod: NameEntity
    {
        public int BudgetYearId { get; set; }
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Navigation Properties
        public BudgetYear BudgetYear { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
