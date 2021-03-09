using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDisabled { get; set; }
    }

    public abstract class NameEntity : BaseEntity
    {
        public string Name { get; set; }
    }

    public abstract class CodeNameEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public abstract class OptionEntity : BaseEntity
    {
        public string Name { get; set; }
        public int? Order { get; set; }        
    }
}
