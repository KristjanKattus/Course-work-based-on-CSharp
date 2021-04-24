using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Club : DomainEntityId
    {
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        public DateTime Since { get; set; } = DateTime.Now;
        
        public DateTime? Until { get; set; }
        
        [MaxLength(128)]
        public string? Description { get; set; }
        
        
        
        
    }
}