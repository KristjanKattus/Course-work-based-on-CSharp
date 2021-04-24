
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.LeagueTeam
{
    public class LeagueTeamCreateEditViewModel
    {
        public PublicApi.DTO.v1.LeagueTeam LeagueTeam { get; set; } = default!;

        public SelectList? LeagueSelectList { get; set; }
        public SelectList? TeamSelectList { get; set; }
    }
}