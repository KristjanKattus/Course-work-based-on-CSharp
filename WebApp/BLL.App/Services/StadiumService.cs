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
    public class StadiumService: BaseEntityService<IAppUnitOfWork, IStadiumRepository, BLLAppDTO.Stadium, DALAppDTO.Stadium>, IStadiumService
    {
        public StadiumService(IAppUnitOfWork serviceUow, IStadiumRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new StadiumMapper(mapper))
        {
        }
    }
}