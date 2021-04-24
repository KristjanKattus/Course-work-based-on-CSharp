using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GameEventMapper: BaseMapper<DAL.App.DTO.GameEvent, Domain.App.Game_Event>
    {
        public GameEventMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}