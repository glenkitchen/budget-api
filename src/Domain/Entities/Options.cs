using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BudgetType : OptionEntity {
        //public BudgetType()
        //{
        //    Budgets = new HashSet<Budget>();
        //}

        //// Navigation Properties
        //public ICollection<Budget> Budgets { get; set; }
    }
    public class BudgetVersion : OptionEntity {
        //public BudgetVersion()
        //{
        //    BudgetYears = new HashSet<BudgetYear>();
        //    Budgets = new HashSet<Budget>();
        //}
        // Navigation Properties
        //public ICollection<BudgetYear> BudgetYears { get; set; }
        //public ICollection<Budget> Budgets { get; set; }
    }
    public class GlAccountType : OptionEntity {
        //public GlAccountType()
        //{
        //    GlAccounts = new HashSet<GlAccount>();
        //}

        //public ICollection<GlAccount> GlAccounts { get; set; }
    }
    public class SalesRegion : OptionEntity {
        //public SalesRegion()
        //{
        //    GlAccounts = new HashSet<GlAccount>();
        //}

        //public ICollection<GlAccount> GlAccounts { get; set; }
    }
}
