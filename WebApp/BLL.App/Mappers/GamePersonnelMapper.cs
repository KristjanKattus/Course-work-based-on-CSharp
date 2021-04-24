using AutoMapper;

namespace BLL.App.Mappers
{
    public class GamePersonnelMapper: BaseMapper<BLL.App.DTO.GamePersonnel, DAL.App.DTO.GamePersonnel>
    {
        public GamePersonnelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}