using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameEventMapper: BaseMapper<PublicApi.DTO.v1.GameEvent, BLL.App.DTO.GameEvent>
    {
        public GameEventMapper(IMapper mapper): base(mapper)
        {
            
        }
    }
}