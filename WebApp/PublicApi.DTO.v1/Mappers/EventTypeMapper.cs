using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class EventTypeMapper: BaseMapper<PublicApi.DTO.v1.EventType, BLL.App.DTO.EventType>
    {
        public EventTypeMapper(IMapper mapper) : base(mapper)
        {
            
        }
    }
}