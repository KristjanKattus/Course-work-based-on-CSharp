using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class LeagueTeam : DomainEntityId
    {
        public Guid LeagueId { get; set; }

        public League? League { get; set; } = default!;

        public Guid TeamId { get; set; }

        public Team? Team { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}