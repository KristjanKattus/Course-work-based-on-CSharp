using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameTeamList
{
    public class GameTeamListCreateViewModel
    {
        public PublicApi.DTO.v1.Person Person { get; set; } = default!;

        public Guid GameTeamId { get; set; }
        public Guid RoleId { get; set; }
        public SelectList? RoleSelectList { get; set; }

    }
}