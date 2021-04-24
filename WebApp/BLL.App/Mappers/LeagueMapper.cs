using AutoMapper;

namespace BLL.App.Mappers
{
    public class LeagueMapper: BaseMapper<BLL.App.DTO.League, DAL.App.DTO.League>
    {
        public LeagueMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}