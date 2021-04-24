using AutoMapper;

namespace BLL.App.Mappers
{
    public class ClubTeamMapper: BaseMapper<BLL.App.DTO.ClubTeam, DAL.App.DTO.ClubTeam>
    {
        public ClubTeamMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}