using System;
using System.Threading.Tasks;
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
    public class LeagueService: BaseEntityService<IAppUnitOfWork, ILeagueRepository, BLLAppDTO.League, DALAppDTO.League>, ILeagueService
    {
        public LeagueService(IAppUnitOfWork serviceUow, ILeagueRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LeagueMapper(mapper))
        {
        }
        
    }
}