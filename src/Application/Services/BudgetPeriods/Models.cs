
using System;

namespace Application.Services.BudgetPeriods
{
    public class BudgetPeriodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetYearId { get; set; }
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    
    public class BudgetPeriodListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
