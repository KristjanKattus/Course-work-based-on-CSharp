using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Event_Type : DomainEntityId
    {

        public Guid NameId { get; set; }
        [MaxLength(32)]
        public LangString Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}