using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class TeamMapper: BaseMapper<DAL.App.DTO.Team, Domain.App.Team>
    {
        public TeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}