﻿using System.Collections.Generic;
using PublicApi.DTO.v1;

namespace WebApp.ViewModels
{
    public class LeagueTableIndexViewModel
    {
        public string? LeagueName { get; set; }
        public List<LeagueTableTeam>? LeagueTableTeams { get; set; }

        public List<PublicApi.DTO.v1.LeagueGame>? LeagueGames { get; set; }
    }
}