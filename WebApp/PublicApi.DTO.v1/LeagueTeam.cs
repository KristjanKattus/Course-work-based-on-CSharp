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

        public PublicApi.DTO.v1.League League { get; set; } = default!;

        public Guid TeamId { get; set; }

        public PublicApi.DTO.v1.Team Team { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}