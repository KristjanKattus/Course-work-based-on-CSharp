using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
   
    public class LeagueMapper: BaseMapper<PublicApi.DTO.v1.League, BLL.App.DTO.League>
    {
        public LeagueMapper(IMapper mapper) : base(mapper)
        {
        }
    } 
}