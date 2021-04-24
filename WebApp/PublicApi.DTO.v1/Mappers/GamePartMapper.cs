using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GamePartMapper: BaseMapper<PublicApi.DTO.v1.GamePart, BLL.App.DTO.GamePart>
    {
        public GamePartMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}