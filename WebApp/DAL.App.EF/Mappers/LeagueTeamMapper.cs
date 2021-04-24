using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class LeagueTeamMapper: BaseMapper<DAL.App.DTO.LeagueTeam, Domain.App.League_Team>
    {
        public LeagueTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}