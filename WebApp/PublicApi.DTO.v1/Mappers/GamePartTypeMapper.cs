using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GamePartTypeMapper: BaseMapper<PublicApi.DTO.v1.GamePartType, BLL.App.DTO.GamePartType>
    {
        public GamePartTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}