﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.TeamPerson
{
    public class TeamPersonCreateEditViewModel
    {
        public PublicApi.DTO.v1.TeamPerson TeamPerson { get; set; } = default!;
        public SelectList? PersonSelectList { get; set; }
        public SelectList? RoleSelectList { get; set; }
        public SelectList? TeamSelectList { get; set; }
    }
}