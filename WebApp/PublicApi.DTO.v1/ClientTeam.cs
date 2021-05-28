using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class ClientTeam
    {
        public PublicApi.DTO.v1.Team Team { get; set; } = default!;

        public PublicApi.DTO.v1.Club? Club { get; set; }

        public List<PublicApi.DTO.v1.TeamPerson>? StaffList { get; set; }
        
        public List<PublicApi.DTO.v1.TeamPerson>? PlayerList { get; set; }
    }
}