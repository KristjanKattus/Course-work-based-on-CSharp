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
    public class GamePersonnelService: BaseEntityService<IAppUnitOfWork, IGamePersonnelRepository, BLLAppDTO.GamePersonnel, DALAppDTO.GamePersonnel>, IGamePersonnelService
    {
        public GamePersonnelService(IAppUnitOfWork serviceUow, IGamePersonnelRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GamePersonnelMapper(mapper))
        {
        }
    }
}