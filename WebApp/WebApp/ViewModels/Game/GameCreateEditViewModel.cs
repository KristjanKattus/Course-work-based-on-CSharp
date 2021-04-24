using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Game
{
    public class GameCreateEditViewModel
    {
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;
        public SelectList? Stadiums { get; set; }
    }
}