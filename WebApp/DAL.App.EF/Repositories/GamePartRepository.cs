using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class GamePartRepository : BaseRepository<DAL.App.DTO.GamePart, Domain.App.Game_Part, AppDbContext>, IGamePartRepository
    {
        public GamePartRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePartMapper(mapper))
        {
        }

        public override async Task<IEnumerable<GamePart>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.GamePartType)
                .Include(g => g.Game)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<GamePart?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.GamePartType)
                .Include(g => g.Game);

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }
    }
}