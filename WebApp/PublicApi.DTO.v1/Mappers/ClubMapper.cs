using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ClubMapper: BaseMapper<PublicApi.DTO.v1.Club, BLL.App.DTO.Club>
    {
        public ClubMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}