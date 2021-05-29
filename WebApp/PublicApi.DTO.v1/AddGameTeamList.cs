using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class AddGameTeamList
    {
        public Guid GameTeamId { get; set; }
        public string? GameTeamName { get; set; }

        public List<PublicApi.DTO.v1.AddGameMember>? PlayerList { get; set; }
        public List<PublicApi.DTO.v1.AddGameMember>? StaffList { get; set; }
    }
}