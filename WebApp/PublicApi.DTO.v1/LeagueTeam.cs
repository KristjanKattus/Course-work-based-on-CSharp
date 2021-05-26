using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class LeagueTeam
    {
        public Guid Id { get; set; }
        
        public Guid LeagueId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTeam), Name = nameof(League))]
        public League? League { get; set; }
        

        public Guid TeamId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTeam), Name = nameof(Team))]
        public Team? Team { get; set; }

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }

        
        [MaxLength(128)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
    }
}