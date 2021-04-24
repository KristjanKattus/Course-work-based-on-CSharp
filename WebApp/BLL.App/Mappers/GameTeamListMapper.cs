using AutoMapper;

namespace BLL.App.Mappers
{
    public class GameTeamListMapper: BaseMapper<BLL.App.DTO.GameTeamList, DAL.App.DTO.GameTeamList>
    {
        public GameTeamListMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}