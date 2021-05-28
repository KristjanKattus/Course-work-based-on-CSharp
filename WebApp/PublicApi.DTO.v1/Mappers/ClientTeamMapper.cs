using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ClientTeamMapper: BaseMapper<PublicApi.DTO.v1.ClientTeam, BLL.App.DTO.ClientTeam>
    {
        public ClientTeamMapper(IMapper mapper) : base(mapper)
        {
        }

    }
}