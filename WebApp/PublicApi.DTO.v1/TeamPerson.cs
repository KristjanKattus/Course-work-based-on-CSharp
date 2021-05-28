using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class TeamPerson
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.TeamPerson), Name = nameof(Person))]
        public PublicApi.DTO.v1.Person? Person { get; set; } = default!;   

        public Guid TeamId { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.TeamPerson), Name = nameof(Team))]
        public PublicApi.DTO.v1.Team? Team { get; set; }  = default!;

        public Guid RoleId { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.TeamPerson), Name = nameof(Role))]
        public PublicApi.DTO.v1.Role? Role { get; set; } = default!;
    }
}