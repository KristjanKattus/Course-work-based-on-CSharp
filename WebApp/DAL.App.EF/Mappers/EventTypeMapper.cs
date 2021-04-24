using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class EventTypeMapper: BaseMapper<DAL.App.DTO.EventType, Domain.App.Event_Type>
    {
        public EventTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}