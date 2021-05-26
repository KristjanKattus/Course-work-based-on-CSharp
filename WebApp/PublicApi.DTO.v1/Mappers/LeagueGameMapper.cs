using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class LeagueGameMapper: BaseMapper<PublicApi.DTO.v1.LeagueGame, BLL.App.DTO.LeagueGame>
    {
        public LeagueGameMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}