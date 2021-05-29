using System;

namespace PublicApi.DTO.v1
{
    public class AddGameTeamListMember
    {
        public PublicApi.DTO.v1.Person Person { get; set; } = default!;

        public Guid GameTeamId { get; set; }
        public Guid RoleId { get; set; }
    }
}