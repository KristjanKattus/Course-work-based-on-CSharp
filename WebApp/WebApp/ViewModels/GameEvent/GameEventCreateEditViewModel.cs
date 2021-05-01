using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameEvent
{
    public class GameEventCreateEditViewModel
    {
        public PublicApi.DTO.v1.GameEvent GameEvent { get; set; } = default!;

        public SelectList? EventTypeSelectList { get; set; }

        public SelectList? GamePartSelectList { get; set; }

        public SelectList? GameSelectList { get; set; }

        public SelectList? GamePersonnelSelectList { get; set; }

        public SelectList? GameTeamSelectList { get; set; }
    }
}