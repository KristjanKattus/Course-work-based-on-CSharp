using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
   
    public class LeagueTeamMapper: BaseMapper<PublicApi.DTO.v1.LeagueTeam, BLL.App.DTO.LeagueTeam>
    {
        public LeagueTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}