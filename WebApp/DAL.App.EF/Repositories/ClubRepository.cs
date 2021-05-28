using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;


namespace DAL.App.EF.Repositories
{
    public class ClubRepository : BaseRepository<DAL.App.DTO.Club, Domain.App.Club, AppDbContext>, IClubRepository
    {
        public ClubRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ClubMapper(mapper))
        {
        }

    }
}