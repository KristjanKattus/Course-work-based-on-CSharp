using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class LeagueMapper: BaseMapper<DAL.App.DTO.League, Domain.App.League>
    {
        public LeagueMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}