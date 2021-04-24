using System;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class TeamPerson
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public PublicApi.DTO.v1.Person Person { get; set; } = default!;   

        public Guid TeamId { get; set; }

        public PublicApi.DTO.v1.Team Team { get; set; }  = default!;

        public Guid RoleId { get; set; }

        public PublicApi.DTO.v1.Role Role { get; set; } = default!;
    }
}