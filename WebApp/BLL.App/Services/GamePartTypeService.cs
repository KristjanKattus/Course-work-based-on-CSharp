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
    public class GamePartTypeService: BaseEntityService<IAppUnitOfWork, IGamePartTypeRepository, BLLAppDTO.GamePartType, DALAppDTO.GamePartType>, IGamePartTypeService
    {
        public GamePartTypeService(IAppUnitOfWork serviceUow, IGamePartTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GamePartTypeMapper(mapper))
        {
        }
    }
}