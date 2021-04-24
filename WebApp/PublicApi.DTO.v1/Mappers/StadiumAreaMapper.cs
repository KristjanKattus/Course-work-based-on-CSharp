using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class StadiumAreaMapper: BaseMapper<PublicApi.DTO.v1.StadiumArea, BLL.App.DTO.StadiumArea>
    {
        public StadiumAreaMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}