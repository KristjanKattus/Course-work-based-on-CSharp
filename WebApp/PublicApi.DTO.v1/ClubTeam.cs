using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class ClubTeam
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.ClubTeam), Name = nameof(Team))]
        public Team? Team { get; set; }

        public Guid ClubId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.ClubTeam), Name = nameof(Club))]
        public Club? Club { get; set; }
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        [MaxLength(128)] public string? Description { get; set; }
    }
}