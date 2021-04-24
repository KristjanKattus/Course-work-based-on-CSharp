using System;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GameTeamList
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public PublicApi.DTO.v1.Person? Person { get; set; }

        public Guid GameTeamId { get; set; }
        public PublicApi.DTO.v1.GameTeam GameTeam { get; set; } = default!;

        public Guid RoleId { get; set; }
        public PublicApi.DTO.v1.Role Role { get; set; } = default!;
        
        public bool InStartingLineup { get; set; }
        public bool Staff { get; set; }
        
        
        
    }
}