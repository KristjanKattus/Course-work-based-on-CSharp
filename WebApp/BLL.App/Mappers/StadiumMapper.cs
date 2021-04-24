using AutoMapper;

namespace BLL.App.Mappers
{
    public class StadiumMapper: BaseMapper<BLL.App.DTO.Stadium, DAL.App.DTO.Stadium>
    {
        public StadiumMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}