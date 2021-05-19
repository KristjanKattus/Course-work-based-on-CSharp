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
        public League? League { get; set; }
        

        public Guid TeamId { get; set; }
        public Team? Team { get; set; }

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}