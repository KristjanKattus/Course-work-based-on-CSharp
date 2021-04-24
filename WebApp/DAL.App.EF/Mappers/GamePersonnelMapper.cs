using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GamePersonnelMapper: BaseMapper<DAL.App.DTO.GamePersonnel, Domain.App.Game_Personnel>
    {
        public GamePersonnelMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}