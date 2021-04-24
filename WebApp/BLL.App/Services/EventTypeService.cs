using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class EventTypeService: BaseEntityService<IAppUnitOfWork, IEventTypeRepository, BLLAppDTO.EventType, DALAppDTO.EventType>, IEventTypeService
    {
        public EventTypeService(IAppUnitOfWork serviceUow, IEventTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new EventTypeMapper(mapper))
        {
        }
    }
}