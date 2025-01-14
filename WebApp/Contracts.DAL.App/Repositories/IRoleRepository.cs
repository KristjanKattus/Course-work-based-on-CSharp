﻿using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRoleRepository : IBaseRepository<DALAppDTO.Role>, IRoleRepositoryCustom<DALAppDTO.Role>
    {
        
    }

    public interface IRoleRepositoryCustom<TEntity>
    {
    }
}