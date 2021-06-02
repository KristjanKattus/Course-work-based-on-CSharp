using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TeamPersonService: BaseEntityService<IAppUnitOfWork, ITeamPersonRepository, BLLAppDTO.TeamPerson, DALAppDTO.TeamPerson>, ITeamPersonService
    {
        public TeamPersonService(IAppUnitOfWork serviceUow, ITeamPersonRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TeamPersonMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.TeamPerson>> GetAllWithTeamIdAsync(Guid teamId)
        {
            return (await ServiceRepository.GetAllWithTeamIdAsync(teamId)).Select(x => Mapper.Map(x))!;
        }
    }
}