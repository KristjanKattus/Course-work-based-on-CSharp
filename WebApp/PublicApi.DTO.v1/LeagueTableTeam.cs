using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class LeagueTableTeam
    {
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(LeagueTeamName))]
        public string LeagueTeamName { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GamesPlayed))]
        public int GamesPlayed { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GamesWon))]

        public int GamesWon { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GamesLost))]
        public int GamesLost { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GamesTied))]
        public int GamesTied { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GoalsScored))]
        public int GoalsScored { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(GoalsConceded))]
        public int GoalsConceded { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.LeagueTableTeam), Name = nameof(Points))]
        public int Points { get; set; }
        
    }
}