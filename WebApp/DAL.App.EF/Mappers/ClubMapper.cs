using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class ClubMapper: BaseMapper<DAL.App.DTO.Club, Domain.App.Club>
    {
        public ClubMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}