using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class LeagueGame
    {
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueGame), Name = "Game")]
        public Game? Game { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueGame), Name = nameof(HomeTeam))]
        public GameTeam? HomeTeam { get; set; }

        public List<GameTeamList>? HomeTeamList { get; set; }


        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueGame), Name = nameof(AwayTeam))]
        public GameTeam? AwayTeam { get; set; }
        
        public List<GameTeamList>? AwayTeamList { get; set; }
    }
}