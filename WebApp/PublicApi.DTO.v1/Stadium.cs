using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Stadium
    {
        public Guid Id { get; set; }
        public Guid AreaId { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Stadium), Name = nameof(StadiumArea))]
        public PublicApi.DTO.v1.StadiumArea StadiumArea { get; set; } = default!;

        [MaxLength(32)] 
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Stadium), Name = nameof(PitchType))]
        public string PitchType { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Stadium), Name = nameof(Category))]
        public int Category { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Stadium), Name = nameof(Games))]

        public ICollection<PublicApi.DTO.v1.Game>? Games { get; set; }

        [MaxLength(128)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
    }
}