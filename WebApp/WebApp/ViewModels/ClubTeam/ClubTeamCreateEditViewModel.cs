using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.ClubTeam
{
    public class ClubTeamCreateEditViewModel
    {
        public PublicApi.DTO.v1.ClubTeam ClubTeam { get; set; } = default!;

        public SelectList? ClubSelectList { get; set; }
        
        public SelectList? TeamlSelectList { get; set; }
    }
}