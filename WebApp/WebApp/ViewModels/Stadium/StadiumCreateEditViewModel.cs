using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Stadium
{
    public class StadiumCreateEditViewModel
    {
        public PublicApi.DTO.v1.Stadium Stadium { get; set; } = default!;
        public SelectList? StadiumAreaSelectList { get; set; }
    }
}