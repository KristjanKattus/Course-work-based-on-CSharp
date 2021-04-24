using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameTeam
{
    public class GameTeamCreateEditViewModel
    {
        public PublicApi.DTO.v1.GameTeam GameTeam { get; set; } = default!;
        public SelectList? GameSelectList { get; set; }
        public SelectList? TeamSelectList { get; set; }
        
    }
}