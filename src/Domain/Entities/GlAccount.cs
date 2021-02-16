using Domain.Common;

namespace Domain.Entities
{
    public class GlAccount: BaseCodeNameEntity
    {
        public int GlAccountTypeId { get; set; }
        public int? SalesRegionId { get; set; }

        // Navigation Properties
        public GlAccountType GlAccountType { get; set; }
        public SalesRegion SalesRegion { get; set; }
    }
}
