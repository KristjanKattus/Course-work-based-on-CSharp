using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class TeamPersonMapper: BaseMapper<DAL.App.DTO.TeamPerson, Domain.App.Team_Person>
    {
        public TeamPersonMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}