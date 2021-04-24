using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GamePartMapper: BaseMapper<DAL.App.DTO.GamePart, Domain.App.Game_Part>
    {
        public GamePartMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}