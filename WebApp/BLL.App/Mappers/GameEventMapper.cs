using AutoMapper;

namespace BLL.App.Mappers
{
    public class GameEventMapper: BaseMapper<BLL.App.DTO.GameEvent, DAL.App.DTO.GameEvent>
    {
        public GameEventMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}