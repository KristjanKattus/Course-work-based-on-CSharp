using AutoMapper;

namespace BLL.App.Mappers
{
    public class LeagueTeamMapper: BaseMapper<BLL.App.DTO.LeagueTeam, DAL.App.DTO.LeagueTeam>
    {
        public LeagueTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}