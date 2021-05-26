using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class StadiumArea
    {
        public Guid Id { get; set; }
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.StadiumArea), Name = nameof(Stadiums))]

        public ICollection<PublicApi.DTO.v1.Stadium>? Stadiums { get; set; }
        
    }
}