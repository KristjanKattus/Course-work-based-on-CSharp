using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class LeagueTableMapper: BaseMapper<PublicApi.DTO.v1.LeagueTableTeam, BLL.App.DTO.LeagueTableTeam>
    {
        public LeagueTableMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}