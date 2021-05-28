using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class ClientTeam
    {
        public BLL.App.DTO.Team Team { get; set; } = default!;

        public BLL.App.DTO.Club? Club { get; set; }

        public List<BLL.App.DTO.TeamPerson>? StaffList { get; set; }
        
        public List<BLL.App.DTO.TeamPerson>? PlayerList { get; set; }
    }
}