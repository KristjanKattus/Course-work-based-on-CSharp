using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class StadiumMapper: BaseMapper<PublicApi.DTO.v1.Stadium, BLL.App.DTO.Stadium>
    {
        public StadiumMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}