namespace PublicApi.DTO.v1
{
    public class LeagueTableTeam
    {
        public string LeagueTeamName { get; set; } = default!;

        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int GamesTied { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int Points { get; set; }
        
    }
}