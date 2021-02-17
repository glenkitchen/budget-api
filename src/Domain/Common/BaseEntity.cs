using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        //Alternate key 
        //public Guid Guid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
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
