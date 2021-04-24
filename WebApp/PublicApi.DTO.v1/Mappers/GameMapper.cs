using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameMapper: BaseMapper<PublicApi.DTO.v1.Game, BLL.App.DTO.Game>
    {
        public GameMapper(IMapper mapper): base(mapper)
        {
            
        }
    }
}