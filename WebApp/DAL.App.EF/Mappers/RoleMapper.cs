using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class RoleMapper: BaseMapper<DAL.App.DTO.Role, Domain.App.Role>
    {
        public RoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}