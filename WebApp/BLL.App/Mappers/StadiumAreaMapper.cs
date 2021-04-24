using AutoMapper;

namespace BLL.App.Mappers
{
    public class StadiumAreaMapper: BaseMapper<BLL.App.DTO.StadiumArea, DAL.App.DTO.StadiumArea>
    {
        public StadiumAreaMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}