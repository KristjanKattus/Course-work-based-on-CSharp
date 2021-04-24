using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameTeamList
{
    public class GameTeamListCreateEditViewModel
    {
        public PublicApi.DTO.v1.GameTeamList GameTeamList { get; set; } = default!;
        public SelectList? PersonSelectList { get; set; }
        public SelectList? RoleSelectList { get; set; }
        public SelectList? GameTeamSelectList { get; set; }
        
    }
}