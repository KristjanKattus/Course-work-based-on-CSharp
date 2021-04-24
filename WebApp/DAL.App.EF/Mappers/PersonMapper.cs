using AutoMapper;
using Contracts.DAL.Base.Mapper;


namespace DAL.App.EF.Mappers
{
    public class PersonMapper : BaseMapper<DAL.App.DTO.Person, Domain.App.Person>
    {
        public PersonMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}