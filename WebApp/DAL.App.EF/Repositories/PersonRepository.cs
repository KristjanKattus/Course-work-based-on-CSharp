using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<DAL.App.DTO.Person, Domain.App.Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonMapper(mapper))
        {
        }
    }
}