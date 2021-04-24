using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GameMapper: BaseMapper<DAL.App.DTO.Game, Domain.App.Game>
    {
        public GameMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}