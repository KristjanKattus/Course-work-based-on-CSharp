using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class EventType
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}