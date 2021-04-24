using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameTeamListMapper: BaseMapper<PublicApi.DTO.v1.GameTeamList, BLL.App.DTO.GameTeamList>
    {
        public GameTeamListMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}