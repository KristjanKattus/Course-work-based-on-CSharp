using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class LeagueTableClient
    {
        public string? LeagueName { get; set; }
        public List<LeagueTableTeam>? LeagueTableTeams { get; set; }

        public List<PublicApi.DTO.v1.LeagueGame>? LeagueGames { get; set; }
    }
}