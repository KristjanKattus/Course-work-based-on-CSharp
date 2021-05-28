using System;
using Domain.Base;

namespace Domain.App
{
    public class Team_Person : DomainEntityId
    {

        public Guid PersonId { get; set; }

        public Person? Person { get; set; } = default!;   

        public Guid TeamId { get; set; }

        public Team? Team { get; set; }  = default!;

        public Guid RoleId { get; set; }

        public Role? Role { get; set; } = default!;
    }
}