using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameTeamList
{
    public class GameTeamListCreateEditViewModel
    {
        public Guid GameTeamId { get; set; }
        public string? GameTeamName { get; set; }

        public List<PublicApi.DTO.v1.AddGameMember>? PlayerList { get; set; }
        public List<PublicApi.DTO.v1.AddGameMember>? StaffList { get; set; }
    }
}