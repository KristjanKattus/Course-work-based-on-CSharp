using System;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GameTeam
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; } 
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;

        public Guid TeamId { get; set; }
        public PublicApi.DTO.v1.Team Team { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }
        
        public int Scored { get; set; }
        public int Conceded { get; set; }
        public int Points { get; set; }



    }
}