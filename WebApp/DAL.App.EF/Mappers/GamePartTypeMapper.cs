using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class GamePartTypeMapper: BaseMapper<DAL.App.DTO.GamePartType, Domain.App.Game_Part_Type>
    {
        public GamePartTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}