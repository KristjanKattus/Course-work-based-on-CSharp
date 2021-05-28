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
    public class ClubTeamService: BaseEntityService<IAppUnitOfWork, IClubTeamRepository, BLLAppDTO.ClubTeam, DALAppDTO.ClubTeam>, IClubTeamService
    {
        public ClubTeamService(IAppUnitOfWork serviceUow, IClubTeamRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ClubTeamMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.ClubTeam> GetClubWithTeamIdAsync(Guid teamId)
        {
            return Mapper.Map(await ServiceRepository.GetClubWithTeamIdAsync(teamId))!;
        }
    }
}