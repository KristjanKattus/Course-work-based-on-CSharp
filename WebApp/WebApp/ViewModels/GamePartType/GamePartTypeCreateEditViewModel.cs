using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GamePartType
{
    public class GamePartTypeCreateEditViewModel
    {
        public PublicApi.DTO.v1.GamePartType GamePartType { get; set; } = default!;
    }
}