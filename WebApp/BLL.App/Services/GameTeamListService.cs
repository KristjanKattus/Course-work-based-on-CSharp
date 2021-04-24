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
    public class GameTeamListService: BaseEntityService<IAppUnitOfWork, IGameTeamListRepository, BLLAppDTO.GameTeamList, DALAppDTO.GameTeamList>, IGameTeamListService
    {
        public GameTeamListService(IAppUnitOfWork serviceUow, IGameTeamListRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameTeamListMapper(mapper))
        {
        }
    }
}