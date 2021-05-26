using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ClubAdd
    {
        [MaxLength(32)]
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
    public class Club
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Name))]
        
        public string Name { get; set; } = default!;
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }
        
        [MaxLength(128)]
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
    }
}