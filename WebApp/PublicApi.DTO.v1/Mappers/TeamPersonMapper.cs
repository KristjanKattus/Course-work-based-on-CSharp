using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class TeamPersonMapper: BaseMapper<PublicApi.DTO.v1.TeamPerson, BLL.App.DTO.TeamPerson>
    {
        public TeamPersonMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}