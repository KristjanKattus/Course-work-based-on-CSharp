using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class League
    {
        public Guid Id { get; set; }
        [MaxLength(32)]
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.League), Name = nameof(Duration))]
        public int Duration { get; set; }

        [MaxLength(128)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
    }
}