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
    public class GamePartService: BaseEntityService<IAppUnitOfWork, IGamePartRepository, BLLAppDTO.GamePart, DALAppDTO.GamePart>, IGamePartService
    {
        public GamePartService(IAppUnitOfWork serviceUow, IGamePartRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GamePartMapper(mapper))
        {
        }
    }
}