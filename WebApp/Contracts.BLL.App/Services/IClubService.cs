using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IClubService : IBaseEntityService<BLLAppDTO.Club, DALAppDTO.Club>, IClubRepositoryCustom<BLLAppDTO.Club>
    {
        
    }
}