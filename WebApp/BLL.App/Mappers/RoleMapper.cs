﻿using AutoMapper;

namespace BLL.App.Mappers
{
    public class RoleMapper: BaseMapper<BLL.App.DTO.Role, DAL.App.DTO.Role>
    {
        public RoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}