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
    public class PersonService: BaseEntityService<IAppUnitOfWork, IPersonRepository, BLLAppDTO.Person, DALAppDTO.Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork serviceUow, IPersonRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PersonMapper(mapper))
        {
        }
    }
}