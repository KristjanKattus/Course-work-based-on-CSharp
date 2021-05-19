using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Game
{
    public class GameCreateEditViewModel
    {
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;
        public SelectList? StadiumSelectList { get; set; }
        
        
        public Guid HomeTeamId { get; set; }
        public SelectList? HomeTeamSelectList { get; set; }
        
        public Guid AwayTeamId { get; set; }
        public SelectList? AwayTeamSelectList { get; set; }
    }
}