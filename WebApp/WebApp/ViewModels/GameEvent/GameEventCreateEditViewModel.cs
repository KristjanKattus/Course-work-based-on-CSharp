using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameEvent
{
    public class GameEventCreateEditViewModel
    {
        public PublicApi.DTO.v1.GameEvent GameEvent { get; set; } = default!;

        public Guid GameId { get; set; }

        public SelectList? EventTypeSelectList { get; set; }

        public Guid HomeTeamId { get; set; }

        public Guid AwayTeamId { get; set; }
        public SelectList? GameTeamListSelectList { get; set; }
    }
}