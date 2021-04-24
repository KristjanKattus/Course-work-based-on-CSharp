using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Team
{
    public class TeamCreateEditViewModel
    {
        public PublicApi.DTO.v1.Team Team { get; set; } = default!;
        public SelectList? TeamPersonSelectList { get; set; }
    }
}