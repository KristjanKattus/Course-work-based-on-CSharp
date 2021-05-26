using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public class DomainEntityMeta: IDomainEntityMeta
    {
        
        [MaxLength(128)]
        public string CreatedBy { get; set; } = "system";
        public DateTime CreatedAt { get; set; }
        
        [MaxLength(128)]
        public string UpdatedBy { get; set; } = "system";
        public DateTime UpdatedAt { get; set; }
    }
}