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
    public class ClubService : BaseEntityService<IAppUnitOfWork, IClubRepository, BLLAppDTO.Club, DALAppDTO.Club>, IClubService
    {
        public ClubService(IAppUnitOfWork serviceUow, IClubRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ClubMapper(mapper))
        {
        }
    }
}