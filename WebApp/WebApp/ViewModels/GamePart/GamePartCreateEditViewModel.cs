using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GamePart
{
    public class GamePartCreateEditViewModel
    {
        public PublicApi.DTO.v1.GamePart GamePart { get; set; } = default!;
        public SelectList? GamePartTypesSelectList { get; set; }
    }
}