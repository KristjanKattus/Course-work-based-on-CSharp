using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GamePersonnelRepository : BaseRepository<DAL.App.DTO.GamePersonnel, Domain.App.Game_Personnel, AppDbContext>, IGamePersonnelRepository
    {
        public GamePersonnelRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePersonnelMapper(mapper))
        {
        }

        public override async Task<IEnumerable<GamePersonnel>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Person)
                .Include(g => g.Role)
                .Include(g => g.Game)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<GamePersonnel?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Person)
                .Include(g => g.Role)
                .Include(g => g.Game);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }
    }
}