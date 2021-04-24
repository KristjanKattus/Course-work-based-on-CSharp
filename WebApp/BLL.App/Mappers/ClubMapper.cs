using AutoMapper;

namespace BLL.App.Mappers
{
    public class ClubMapper: BaseMapper<BLL.App.DTO.Club, DAL.App.DTO.Club>
    {
        public ClubMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}