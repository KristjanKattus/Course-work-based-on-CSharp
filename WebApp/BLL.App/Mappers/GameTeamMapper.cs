using AutoMapper;

namespace BLL.App.Mappers
{
    public class GameTeamMapper: BaseMapper<BLL.App.DTO.GameTeam, DAL.App.DTO.GameTeam>
    {
        public GameTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}