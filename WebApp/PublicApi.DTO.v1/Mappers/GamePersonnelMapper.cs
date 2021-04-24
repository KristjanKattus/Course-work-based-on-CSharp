using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{

    public class GamePersonnelMapper: BaseMapper<PublicApi.DTO.v1.GamePersonnel, BLL.App.DTO.GamePersonnel>
    {
        public GamePersonnelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}