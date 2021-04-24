using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Team
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }

        public ICollection<PublicApi.DTO.v1.TeamPerson>? TeamPersons { get; set; }
    }
}