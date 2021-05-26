using System;

namespace BLL.App.DTO
{
    public class LeagueGame
    {
        public Game? Game { get; set; }
        
        public GameTeam? HomeTeam { get; set; }
        
        public GameTeam? AwayTeam { get; set; }
    }
}