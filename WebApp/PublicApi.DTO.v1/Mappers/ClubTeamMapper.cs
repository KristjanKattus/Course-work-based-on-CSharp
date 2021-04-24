using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ClubTeamMapper: BaseMapper<PublicApi.DTO.v1.ClubTeam, BLL.App.DTO.ClubTeam>
    {
        public ClubTeamMapper(IMapper mapper) : base(mapper)
        {
            
        }
    }
}