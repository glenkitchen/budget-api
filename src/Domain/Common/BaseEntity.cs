using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        //public Guid Guid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
    }

    public abstract class BaseNameEntity : BaseEntity
    {
        public string Name { get; set; }
    }

    public abstract class BaseCodeNameEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public abstract class BaseOption : BaseEntity
    {
        public string Name { get; set; }
        public int? Order { get; set; }
        
    }
}
