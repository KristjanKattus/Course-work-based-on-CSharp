using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class LeagueTableClient
    {
        public string? LeagueName { get; set; }
        public List<BLL.App.DTO.LeagueTableTeam>? LeagueTableTeams { get; set; }

        public List<BLL.App.DTO.LeagueGame>? LeagueGames { get; set; }
    }
}