using System;
using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class LeagueGame
    {
        public Game? Game { get; set; }
        
        public GameTeam? HomeTeam { get; set; }
        public List<GameTeamList>? HomeTeamList { get; set; }
        
        public GameTeam? AwayTeam { get; set; }
        public List<GameTeamList>? AwayTeamList { get; set; }
    }
}