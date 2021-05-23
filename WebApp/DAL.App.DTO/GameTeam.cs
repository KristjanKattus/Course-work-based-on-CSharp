using System;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameTeam : DomainEntityId
    {
        public Guid GameId { get; set; } 
        public Game? Game { get; set; }

        public Guid TeamId { get; set; }
        public Team? Team { get; set; }
        public string? TeamName { get; set; }

        public DateTime Since { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }
        
        public bool Hometeam { get; set; }
        public int Scored { get; set; }
        public int Conceded { get; set; }
        public int Points { get; set; }



    }
}