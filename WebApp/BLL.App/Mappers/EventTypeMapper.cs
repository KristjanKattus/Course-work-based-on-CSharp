using AutoMapper;

namespace BLL.App.Mappers
{
    public class EventTypeMapper: BaseMapper<BLL.App.DTO.EventType, DAL.App.DTO.EventType>
    {
        public EventTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}