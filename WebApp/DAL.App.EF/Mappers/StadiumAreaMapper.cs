using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class StadiumAreaMapper: BaseMapper<DAL.App.DTO.StadiumArea, Domain.App.Stadium_Area>
    {
        public StadiumAreaMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}