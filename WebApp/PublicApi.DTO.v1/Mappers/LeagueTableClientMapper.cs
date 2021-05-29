using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class LeagueTableClientMapper : BaseMapper<PublicApi.DTO.v1.LeagueTableClient, BLL.App.DTO.LeagueTableClient>
    {
        public LeagueTableClientMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}