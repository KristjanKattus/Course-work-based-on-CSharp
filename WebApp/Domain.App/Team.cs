using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Team : DomainEntityId
    {
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }

        public ICollection<Team_Person>? TeamPersons { get; set; }
    }
}