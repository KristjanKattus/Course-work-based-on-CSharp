using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Role
    {
        public Guid Id { get; set; }
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        [MaxLength(128)] public string? Description { get; set; }
    }
}