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
    public class StadiumAreaService: BaseEntityService<IAppUnitOfWork, IStadiumAreaRepository, BLLAppDTO.StadiumArea, DALAppDTO.StadiumArea>, IStadiumAreaService
    {
        public StadiumAreaService(IAppUnitOfWork serviceUow, IStadiumAreaRepository serviceRepository, IMapper mapper): base(serviceUow, serviceRepository, new StadiumAreaMapper(mapper))
        {
        }
    }
}