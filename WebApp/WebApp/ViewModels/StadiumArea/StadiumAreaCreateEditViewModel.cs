using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.StadiumArea
{
    public class StadiumAreaCreateEditViewModel
    {
        public PublicApi.DTO.v1.StadiumArea StadiumArea { get; set; } = default!;
        public SelectList? StadiumSelectList { get; set; }
    }
}