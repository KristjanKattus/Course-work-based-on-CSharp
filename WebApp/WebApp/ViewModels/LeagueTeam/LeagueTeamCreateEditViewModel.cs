using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.LeagueTeam
{
    public class LeagueTeamCreateEditViewModel
    {
        public League_Team LeagueTeam { get; set; } = default!;

        public SelectList? LeaguesSelectList { get; set; }
        public SelectList? TeamsSelectList { get; set; }
    }
}