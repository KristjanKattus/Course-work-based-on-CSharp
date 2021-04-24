using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class EventType : DomainEntityId
    {
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}