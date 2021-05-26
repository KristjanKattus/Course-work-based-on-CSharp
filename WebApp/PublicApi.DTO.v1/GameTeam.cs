using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GameTeam
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Game))]
        public Game? Game { get; set; }

        public Guid TeamId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Team))]
        public Team? Team { get; set; }
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Hometeam))]
        
        public bool Hometeam { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Scored))]
        public int Scored { get; set; } 
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Conceded))]
        public int Conceded { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeam), Name = nameof(Points))]
        public int Points { get; set; }



    }
}