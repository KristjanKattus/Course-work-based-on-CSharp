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
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
        
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Team), Name = nameof(TeamPersons))]
        public ICollection<PublicApi.DTO.v1.TeamPerson>? TeamPersons { get; set; }
    }
}