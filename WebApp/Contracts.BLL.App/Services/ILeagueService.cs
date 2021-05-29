using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ILeagueService : IBaseEntityService<BLLAppDTO.League, DALAppDTO.League>, ILeagueRepositoryCustom<BLLAppDTO.League>
    {
    }
}