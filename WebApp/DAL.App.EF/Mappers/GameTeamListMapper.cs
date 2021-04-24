using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GameTeamListMapper: BaseMapper<DAL.App.DTO.GameTeamList, Domain.App.Game_Team_List>
    {
        public GameTeamListMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}