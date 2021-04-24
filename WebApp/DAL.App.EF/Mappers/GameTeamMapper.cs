using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GameTeamMapper: BaseMapper<DAL.App.DTO.GameTeam, Domain.App.Game_Team>
    {
        public GameTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}