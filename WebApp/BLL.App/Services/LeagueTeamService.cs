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
    public class LeagueTeamService: BaseEntityService<IAppUnitOfWork, ILeagueTeamRepository, BLLAppDTO.LeagueTeam, DALAppDTO.LeagueTeam>, ILeagueTeamService
    {
        public LeagueTeamService(IAppUnitOfWork serviceUow, ILeagueTeamRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LeagueTeamMapper(mapper))
        {
        }
    }
}