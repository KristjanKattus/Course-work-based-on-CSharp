using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameTeamMapper: BaseMapper<PublicApi.DTO.v1.GameTeam, BLL.App.DTO.GameTeam>
    {
        public GameTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}