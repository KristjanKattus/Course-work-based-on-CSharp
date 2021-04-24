using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class StadiumMapper: BaseMapper<DAL.App.DTO.Stadium, Domain.App.Stadium>
    {
        public StadiumMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}