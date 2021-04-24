using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class ClubTeamMapper: BaseMapper<DAL.App.DTO.ClubTeam, Domain.App.Club_Team>
    {
        public ClubTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}