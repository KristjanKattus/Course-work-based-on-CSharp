using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GamePartType
{
    public class GamePersonnelCreateEditViewModel
    {
        public PublicApi.DTO.v1.GamePersonnel GamePersonnel { get; set; } = default!;
        public SelectList? Roles { get; set; }
        public SelectList? Persons { get; set; }
        
    }
}