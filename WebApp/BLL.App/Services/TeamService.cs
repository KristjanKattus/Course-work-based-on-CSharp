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
    public class TeamService: BaseEntityService<IAppUnitOfWork, ITeamRepository, BLLAppDTO.Team, DALAppDTO.Team>, ITeamService
    {
        public TeamService(IAppUnitOfWork serviceUow, ITeamRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TeamMapper(mapper))
        {
        }
    }
}